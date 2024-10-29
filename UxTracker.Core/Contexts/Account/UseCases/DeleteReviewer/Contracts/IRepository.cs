using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.DeleteReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
    Task DeleteUserAsync(Reviewer user, CancellationToken cancellationToken);
}