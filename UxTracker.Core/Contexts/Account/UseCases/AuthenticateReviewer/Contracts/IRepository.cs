using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> AnyProjectAsync(string id, CancellationToken cancellationToken);
}