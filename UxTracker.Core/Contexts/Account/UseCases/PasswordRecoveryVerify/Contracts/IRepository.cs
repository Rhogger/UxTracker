using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task ValidateResetCodeAsync(User user, CancellationToken cancellationToken);
}