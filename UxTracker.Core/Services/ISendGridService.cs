namespace UxTracker.Core.Services;

public interface ISendGridService
{
    public Task SendEmail(string email, string? code, string templateId, CancellationToken cancellationToken);
}