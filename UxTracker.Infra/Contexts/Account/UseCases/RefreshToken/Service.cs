using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.RefreshToken.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.RefreshToken;

public class Service : IService
{
    private readonly JwtService _jwtService = new();

    public string? GenerateAccessToken(User user, CancellationToken cancellationToken)
    {
        var payload = new Payload
        {
            Id = user.Id.ToString(),
            Roles = user.Roles.Select(x => x?.Name).ToArray()
        };

        return _jwtService.Generate(payload);
    }

    public string GenerateRefreshToken(User user, CancellationToken cancellationToken)   
    {
        var payload = new Payload
        {
            Id = user.Id.ToString(),
        };

        return _jwtService.GenerateRefresh(payload);
    }
}