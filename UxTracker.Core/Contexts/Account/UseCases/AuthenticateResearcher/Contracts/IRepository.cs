using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateResearcher.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}