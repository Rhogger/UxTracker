using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

    Task UpdateVerificationCodeAsync(User user, CancellationToken cancellationToken);
}