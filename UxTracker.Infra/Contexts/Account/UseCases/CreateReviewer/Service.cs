using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.CreateReviewer.Contracts;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.CreateReviewer;

public class Service : IService
{
    private readonly SendGridService _sendGridService = new();
    public async Task SendVerificationEmailAsync(Reviewer user, CancellationToken cancellationToken)
    {
        await _sendGridService.SendEmail(user.Email.Address, user.Email.Verification.Code, "d-3a645d8a33bc4a50808ac69df827ed93", cancellationToken);
    }
}