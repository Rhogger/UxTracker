using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task ValidateResetCodeAsync(Researcher user, CancellationToken cancellationToken);
}