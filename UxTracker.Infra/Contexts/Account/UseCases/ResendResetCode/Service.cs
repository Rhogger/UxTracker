using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendResetCode.Contracts;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.ResendResetCode;

public class Service: IService
{
    private readonly SendGridService _sendGridService = new();
    public async Task ResendResetCodeAsync(User user, CancellationToken cancellationToken)
    {
        await _sendGridService.SendEmail(user, user.Password.ResetCode, "d-3a645d8a33bc4a50808ac69df827ed93", cancellationToken);
    }
}