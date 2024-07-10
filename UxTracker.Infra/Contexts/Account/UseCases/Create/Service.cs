using SendGrid;
using SendGrid.Helpers.Mail;
using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Create.Contracts;

namespace UxTracker.Infra.Contexts.Account.UseCases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(
            Configuration.Email.DefaultFromEmail,
            Configuration.Email.DefaultFromName
        );
        var to = new EmailAddress(user.Email, user.Name);
        
        var msg = new SendGridMessage();
        msg.SetFrom(from);
        msg.AddTo(to);
        msg.SetTemplateId("d-3a645d8a33bc4a50808ac69df827ed93");
        msg.SetTemplateData(new
        {
            verification_code = user.Email.Verification.Code
        });

        await client.SendEmailAsync(msg, cancellationToken);
    }
}
