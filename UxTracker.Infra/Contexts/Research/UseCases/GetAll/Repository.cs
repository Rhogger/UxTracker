using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Research.UseCases.GetAll.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetAll;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<List<GetAllDto>?> GetProjectsByUser(string userId, CancellationToken cancellationToken) => 
        await context
            .Projects
            .AsNoTracking()
            .Where(x => x.UserId.ToString() == userId)
            .Select(x => new GetAllDto
            {
                Id = x.Id,
                Title = x.Title,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                ReviewsCount = x.Reviews.Count,
                ReviewersCount = x.Reviews.Count > 0 
                    ? x.Reviews
                        .GroupBy(rate => rate.UserId)
                        .Count()
                    : 0,
            })
            .OrderBy(x => x.Status == Status.InProgress ? 0 
                : x.Status == Status.Finished ? 1 
                : 2)
            .ThenByDescending(x => x.StartDate)
            .ThenByDescending(x => x.EndDate)
            .ToListAsync(cancellationToken);
}