using UxTracker.Core.Contexts.Research.DTOs;

namespace UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;

public interface IRepository
{
    Task<GetForReviewDTO?> GetProjectsByIdAsync(string projectId, CancellationToken cancellationToken);
    Task<bool> IsTermAcceptedAsync(string userId, string projectId, CancellationToken cancellationToken);
}