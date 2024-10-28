using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;
using UxTracker.Core.Contexts.Review.ValueObjects;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetForReview;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<GetForReviewDto?> GetProjectsByIdAsync(string userId, string projectId,
        CancellationToken cancellationToken)
    {
        var project = await context
            .Projects
            .AsNoTracking()
            .Where(x => x.Id.ToString().Equals(projectId))
            .Select(x => new GetForReviewDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                PeriodType = x.PeriodType,
                SurveyCollections = x.SurveyCollections,
                Reviews = x.Reviews
                    .Where(rate => rate.UserId.ToString().Equals(userId))
                    .OrderBy(r => r.RatedAt)
                    .Select(rate => new UserRates
                    {
                        Rate = rate.Rating,
                        Comment = rate.Comment,
                        RatedAt = rate.RatedAt
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

        return new GetForReviewDto
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
        await context
            .AcceptedTerms
            .AsNoTracking()
            .AnyAsync(x => x.UserId.ToString() == userId && x.ProjectId.ToString() == projectId, cancellationToken);
}