using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;
using UxTracker.Core.Contexts.Review.ValueObjects;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetForReview;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<GetForReviewDTO?> GetProjectsByIdAsync(string userId, string projectId,
        CancellationToken cancellationToken)
    {
        var project = await _context
            .Projects
            .AsNoTracking()
            .Where(x => x.Id.ToString().Equals(projectId))
            .Select(x => new GetForReviewDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                PeriodType = x.PeriodType,
                SurveyCollections = x.SurveyCollections,
                Reviews = x.Reviews
                    .Where(x => x.UserId.ToString().Equals(userId))
                    .OrderBy(r => r.RatedAt)
                    .Select(x => new UserRates
                    {
                        Rate = x.Rating,
                        Comment = x.Comment,
                        RatedAt = x.RatedAt
                    }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
        
        if (project == null)
        {
            return null;
        }

        var indexedReviews = project.Reviews
            .Select((review, index) => new UserRates
            {
                Rate = review.Rate,
                Comment = review.Comment,
                RatedAt = review.RatedAt,
                Index = index
            }).ToList();

        return new GetForReviewDTO
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
            PeriodType = project.PeriodType,
            SurveyCollections = project.SurveyCollections,
            Reviews = indexedReviews
        };
    }

    public async Task<bool> IsTermAcceptedAsync(string userId, string projectId, CancellationToken cancellationToken) =>
        await _context
            .AcceptedTerms
            .AsNoTracking()
            .AnyAsync(x => x.UserId.ToString() == userId && x.ProjectId.ToString() == projectId, cancellationToken);
}