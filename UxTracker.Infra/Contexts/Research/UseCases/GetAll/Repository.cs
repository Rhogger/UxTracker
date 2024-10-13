using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Research.UseCases.GetAll.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetAll;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<List<GetAllDTO>?> GetProjectsByUser(string userId, CancellationToken cancellationToken) => 
        await _context
            .Projects
            .AsNoTracking()
            .Where(x => x.UserId.ToString() == userId)
            .Select(x => new GetAllDTO
            {
                Id = x.Id,
                Title = x.Title,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                ReviewsCount = x.Reviews.Count,
                ReviewersCount = x.Reviews.Count > 0 
                    ? x.Reviews
                        .GroupBy(x => x.UserId)
                        .Select(x => x.Count())
                        .FirstOrDefault()
                    : 0,
            })
            .OrderBy(x => x.Status == Status.InProgress ? 0 
                : x.Status == Status.Finished ? 1 
                : 2)
            .ThenByDescending(x => x.StartDate)
            .ThenByDescending(x => x.EndDate)
            .ToListAsync(cancellationToken);
}