using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
    Task UpdateAccountAsync(Reviewer user, CancellationToken cancellationToken);
}