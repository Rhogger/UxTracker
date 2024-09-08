using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.RefreshToken.Contracts;

public interface IService
{
    string GenerateAccessToken(User user, CancellationToken cancellationToken);
    string GenerateRefreshToken(User user, CancellationToken cancellationToken);
}