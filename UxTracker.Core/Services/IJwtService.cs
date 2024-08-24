using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Services;

public interface IJwtService
{
    public string Generate(Payload data);
}