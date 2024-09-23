using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery.Contracts;

public interface IService
{
    Task SendResetCodeAsync(Researcher user, CancellationToken cancellationToken);
}