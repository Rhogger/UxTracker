using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.Get.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Get;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<GetDto?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => 
        await context
            .Projects
            .AsNoTracking()
            .Include(x => x.Relatories)
            .Include(x => x.Reviews)
            .Where(x => x.Id.ToString().Equals(id))
            .Select(x => new GetDto
            {
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                PeriodType = x.PeriodType,
                SurveyCollections = x.SurveyCollections,
                ClusterNumber = x.ClusterNumber,
                LastSurveyCollection = x.Reviews.Count > 0
                    ? x.Reviews
                        .GroupBy(rate => rate.UserId)
                        .Select(rates => rates.Count())
                        .Max()
                    : 0,
                ReviewsCount = x.Reviews.Count,
                ReviewersCount = x.Reviews.Count > 0 
                    ? x.Reviews
                        .GroupBy(rate => rate.UserId)
                        .Count()
                    : 0,
                Relatories = x.Relatories.Select(relatory => new GetRelatoriesDto
                {
                    Id = relatory.Id,
                    Title = relatory.Title,
                }).ToList(),
                Reviews = x.Reviews.Count > 0
                    ? x.Reviews
                        .OrderBy(rate => rate.RatedAt)
                        .Select(rate => new ReviewsDto
                        {
                            UserId = rate.UserId,
                            Email = rate.User.Email,
                            Index = rate.Index,
                            Rate = rate.Rating,
                            Comment = rate.Comment,
                        }).ToList()
                    : new List<ReviewsDto>(),
            }).FirstOrDefaultAsync(cancellationToken);
}