using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.Handlers;

public interface ICookieHandler
{
    public Task RemoveAuthTokenAsync();
    public Task SaveAuthToken(string? token);
    public Task<Cookie?> GetAuthToken();
}