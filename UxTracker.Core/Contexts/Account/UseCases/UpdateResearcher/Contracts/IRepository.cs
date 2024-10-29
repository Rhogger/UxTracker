using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByIdAsync(string? id, CancellationToken cancellationToken);
    Task UpdateAccountAsync(Researcher user, CancellationToken cancellationToken);
}