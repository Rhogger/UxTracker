using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.RefreshToken.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
}