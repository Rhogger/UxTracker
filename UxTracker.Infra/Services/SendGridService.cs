using SendGrid;
using SendGrid.Helpers.Mail;
using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Services;

namespace UxTracker.Infra.Services;

public class SendGridService: ISendGridService
{
    public async Task SendEmail(User user, string code, string templateId, CancellationToken cancellationToken)
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
        msg.SetTemplateId(templateId);
        msg.SetTemplateData(new
        {
            code,
        });

        await client.SendEmailAsync(msg, cancellationToken);
    }
}