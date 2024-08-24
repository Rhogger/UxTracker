using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Authenticate.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.Authenticate;

public class Service : IService
{
    private readonly JwtService _jwtService = new();

    public string GenerateJwtToken(User user, CancellationToken cancellationToken)
    {
        var payload = new Payload
        {
            Id = user.Id.ToString(),
        };

        return _jwtService.Generate(payload);
    }
}