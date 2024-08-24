using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Verify.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Infra.Services;

namespace UxTracker.Infra.Contexts.Account.UseCases.Verify;

public class Service : IService
{
    private readonly JwtService _jwtService = new();

    public Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken)
    {
        var payload = new Payload
        {
            Id = user.Id.ToString(),
        };
        
        return Task.FromResult(_jwtService.Generate(payload));
    }
}