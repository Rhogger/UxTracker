using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByIdAsync(string id, CancellationToken cancellationToken);
    Task DeleteUserAsync(Researcher user, CancellationToken cancellationToken);
}