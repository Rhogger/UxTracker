using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher.Contracts;

public interface IService
{
    Task ResendVerificationCodeAsync(Researcher user, CancellationToken cancellationToken);
}