using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateAccount.Contracts;

public interface IRepository
{
    Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
    Task UpdateAccountAsync(User user, CancellationToken cancellationToken);
}