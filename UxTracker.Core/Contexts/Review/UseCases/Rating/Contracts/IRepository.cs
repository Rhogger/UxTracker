using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.DTO;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.Core.Contexts.Review.UseCases.Rating.Contracts;

public interface IRepository
{
    Task RatingAsync(Rate rate, CancellationToken cancellationToken);
    Task<ProjectValidInfoDTO?> GetInfosFromProjectAsync(string projectId, CancellationToken cancellationToken);
    Task<List<Rate>?> GetReviewsByUserAsync(string userId, string projectId, CancellationToken cancellationToken);
}