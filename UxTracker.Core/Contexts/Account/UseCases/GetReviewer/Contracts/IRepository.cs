using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.GetReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
}