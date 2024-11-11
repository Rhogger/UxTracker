using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Services;

namespace UxTracker.Web.Handlers;

public class CookieHandler(ICookieService cookieService): ICookieHandler
{
    private static readonly string? AccessCookieName = Configuration.Cookie.AccessTokenCookieName;
    private static readonly string? RefreshCookieName = Configuration.Cookie.RefreshTokenCookieName;

    private static Cookie CreateCookie(string? cookieName,string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("O token é vazio.");

        return new Cookie(cookieName, token);
    }

    public async Task SaveAccessToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("O token é vazio.");

        var cookie = CreateCookie(AccessCookieName, token);
        
        await cookieService.SetAsync(cookie);
    }
    
    public async Task SaveRefreshToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("O token é vazio.");

        var cookie = CreateCookie(RefreshCookieName, token);
        
        cookie.Expiration = DateTime.UtcNow.AddDays(6); 
        
        await cookieService.SetAsync(cookie);
    }

    public async Task RemoveAccessTokenAsync()
    {
        await cookieService.RemoveAsync(AccessCookieName);
    }
    
    public async Task RemoveRefreshTokenAsync()
    {
        await cookieService.RemoveAsync(RefreshCookieName);
    }

    public async Task<Cookie?> GetAccessToken() => await cookieService.GetAsync(AccessCookieName);
    public async Task<Cookie?> GetRefreshToken() => await cookieService.GetAsync(RefreshCookieName);
}