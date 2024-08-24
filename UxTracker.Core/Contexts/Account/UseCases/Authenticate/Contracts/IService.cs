using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.Authenticate.Contracts;

public interface IService
{
    string GenerateJwtToken(User user, CancellationToken cancellationToken);
}