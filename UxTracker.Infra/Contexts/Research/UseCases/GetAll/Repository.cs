using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetAll.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetAll;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<List<GetAllProjectDTO>?> GetProjectsByUser(string userId, CancellationToken cancellationToken) => 
        await _context
            .Projects
            .AsNoTracking()
            .Where(x => x.UserId.ToString() == userId)
            .Select(x => new GetAllProjectDTO
            {
                Id = x.Id,
                Title = x.Title,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                ReviewersCount = x.ReviewersCount,
                SurveyCollections = x.SurveyCollections
            })
            .ToListAsync(cancellationToken);
}