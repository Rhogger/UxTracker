using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdatePassword.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(User user, string plainTextPassword, CancellationToken cancellationToken);
}