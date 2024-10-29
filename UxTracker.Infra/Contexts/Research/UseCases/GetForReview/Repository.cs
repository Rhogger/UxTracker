using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;
using UxTracker.Core.Contexts.Review.ValueObjects;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetForReview;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<GetForReviewDto?> GetProjectsByIdAsync(string userId, string projectId,
        CancellationToken cancellationToken) =>
        await context
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
                        Index = rate.Index,
                        Rate = rate.Rating,
                        Comment = rate.Comment,
                        RatedAt = rate.RatedAt
                    }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    
    public async Task<bool> IsTermAcceptedAsync(string userId, string projectId, CancellationToken cancellationToken) =>
        await context
            .AcceptedTerms
            .AsNoTracking()
            .AnyAsync(x => x.UserId.ToString() == userId && x.ProjectId.ToString() == projectId, cancellationToken);
}