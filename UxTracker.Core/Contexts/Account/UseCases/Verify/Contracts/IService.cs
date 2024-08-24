using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify.Contracts;

public interface IService
{
    Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken);
}