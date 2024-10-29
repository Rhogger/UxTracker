using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdatePassword.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(Researcher user, string plainTextPassword, CancellationToken cancellationToken);
}