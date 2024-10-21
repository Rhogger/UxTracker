using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.UseCases.Rating.Contracts;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Review.UseCases.Rating;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task RatingAsync(Rate rate, CancellationToken cancellationToken)
    {
        await _context.Reviews.AddAsync(rate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PeriodType?> GetPeriodTypeFromProjectAsync(string projectId, CancellationToken cancellationToken) =>
        await _context
            .Projects
            .AsNoTracking()
            .Where(x => x.Id.ToString().Equals(projectId))
            .Select(x => x.PeriodType)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    public async Task<List<Rate>?> GetReviewsByUserAsync(string userId, string projectId,
        CancellationToken cancellationToken) =>
        await _context
            .Reviews
            .AsNoTracking()
            .Where(x => x.UserId.ToString() == userId)
            .ToListAsync(cancellationToken);
}