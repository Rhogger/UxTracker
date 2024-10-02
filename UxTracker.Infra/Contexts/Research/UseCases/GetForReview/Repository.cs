using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetForReview;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<GetForReviewDTO?> GetProjectsByIdAsync(string projectId, CancellationToken cancellationToken) => 
        await _context
            .Projects
            .AsNoTracking()
            .Select(x => new GetForReviewDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                StartDate = x.StartDate,
                PeriodType = x.PeriodType,
                SurveyCollections = x.SurveyCollections
            })
            .FirstOrDefaultAsync(x => x.Id.ToString() == projectId,cancellationToken);

    public async Task<bool> IsTermAcceptedAsync(string userId, string projectId, CancellationToken cancellationToken) =>
        await _context
            .AcceptedTerms
            .AsNoTracking()
            .AnyAsync(x => x.UserId.ToString() == userId && x.ProjectId.ToString() == projectId, cancellationToken);
}