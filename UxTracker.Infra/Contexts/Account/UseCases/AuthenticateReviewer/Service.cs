using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.AuthenticateReviewer;

public class Service : IService
{
    private readonly JwtService _jwtService = new();

    public string GenerateAccessToken(Reviewer user, CancellationToken cancellationToken)
    {
        var payload = new Payload
        {
            Id = user.Id.ToString(),
            Roles = user.Roles.Select(x => x.Name).ToArray()
        };

        return _jwtService.Generate(payload);
    }

    public string GenerateRefreshToken(Reviewer user, CancellationToken cancellationToken)   
    {
        var payload = new Payload
        {
            Id = user.Id.ToString(),
        };

        return _jwtService.GenerateRefresh(payload);
    }
}