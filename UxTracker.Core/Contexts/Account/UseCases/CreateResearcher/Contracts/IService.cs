using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateResearcher.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}