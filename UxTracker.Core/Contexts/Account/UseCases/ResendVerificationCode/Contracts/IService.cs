using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode.Contracts;

public interface IService
{
    Task ResendVerificationCodeAsync(User user, CancellationToken cancellationToken);
}