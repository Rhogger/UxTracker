using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Review.DTO;
using UxTracker.Core.Contexts.Review.UseCases.Rating.Contracts;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Review.UseCases.Rating;

public class Repository(AppDbContext context) : IRepository
{
    public async Task RatingAsync(Rate rate, CancellationToken cancellationToken)
    {
        await context.Reviews.AddAsync(rate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProjectValidInfoDto?> GetInfosFromProjectAsync(string projectId, CancellationToken cancellationToken) =>
        await context
            .Projects
            .AsNoTracking()
            .Where(x => x.Id.ToString().Equals(projectId))
            .Select(x => new ProjectValidInfoDto
            {
                Status = x.Status,
                PeriodType = x.PeriodType,
                SurveyCollections = x.SurveyCollections
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    
    public async Task<List<Rate>?> GetReviewsByUserAsync(string userId, string projectId,
        CancellationToken cancellationToken) =>
        await context
            .Reviews
            .AsNoTracking()
            .Where(x => x.UserId.ToString() == userId)
            .ToListAsync(cancellationToken);
}