using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.RefreshToken.Contracts;

public interface IRepository
{
    Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
}