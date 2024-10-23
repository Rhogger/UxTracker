using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.Get.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Get;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    } 

    public async Task<GetDTO?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => 
        await _context
            .Projects
            .AsNoTracking()
            .Include(x => x.Relatories)
            .Include(x => x.Reviews)
            .Where(x => x.Id.ToString().Equals(id))
            .Select(x => new GetDTO
            {
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                PeriodType = x.PeriodType,
                SurveyCollections = x.SurveyCollections,
                LastSurveyCollection = x.Reviews.Count > 0 
                    ? x.Reviews
                        .GroupBy(x => x.UserId)
                        .Select(x => x.Count())
                        .Max()
                    : 0,
                ReviewsCount = x.Reviews.Count,
                ReviewersCount = x.Reviews.Count > 0 
                    ? x.Reviews
                        .GroupBy(x => x.UserId)
                        .Select(x => x.Count())
                        .FirstOrDefault()
                    : 0,
                Relatories = x.Relatories.Select(x => new GetRelatoriesDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                }).ToList(),
                Reviews = x.Reviews.Select(x => new ReviewsDTO
                {
                    UserId = x.UserId,
                    Email = x.User.Email,
                    Rate = x.Rating,
                    Comment = x.Comment,
                }).ToList(),
            }).FirstOrDefaultAsync(cancellationToken);
}