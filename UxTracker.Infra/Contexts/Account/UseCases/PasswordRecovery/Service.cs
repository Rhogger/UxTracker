using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery.Contracts;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.PasswordRecovery;

public class Service: IService
{
    private readonly SendGridService _sendGridService = new();
    public async Task SendResetCodeAsync(User user, CancellationToken cancellationToken)
    {
        await _sendGridService.SendEmail(user, user.Password.ResetCode, "d-047dd00bf3e74bc99fefff1fd3f4ce4a", cancellationToken);
    }
}