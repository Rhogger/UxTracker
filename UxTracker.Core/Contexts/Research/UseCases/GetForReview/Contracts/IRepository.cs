using UxTracker.Core.Contexts.Research.DTOs;

namespace UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;

public interface IRepository
{
    Task<GetForReviewDto?> GetProjectsByIdAsync(string userId, string projectId, CancellationToken cancellationToken);
    Task<bool> IsTermAcceptedAsync(string userId, string projectId, CancellationToken cancellationToken);
}