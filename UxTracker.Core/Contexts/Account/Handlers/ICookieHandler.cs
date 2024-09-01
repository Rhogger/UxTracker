using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.Handlers;

public interface ICookieHandler
{
    public Task RemoveAccessTokenAsync();
    public Task SaveAccessToken(string? token);
    public Task<Cookie?> GetAccessToken();    
    public Task RemoveRefreshTokenAsync();
    public Task SaveRefreshToken(string? token);
    public Task<Cookie?> GetRefreshToken();
}