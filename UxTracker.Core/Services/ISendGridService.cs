using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Services;

public interface ISendGridService
{
    public Task SendEmail(User user, string code, string templateId, CancellationToken cancellationToken);
}