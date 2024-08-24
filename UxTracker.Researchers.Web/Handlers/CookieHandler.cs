using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Services;

namespace UxTracker.Researchers.Web.Handlers;

public class CookieHandler(ICookieService cookieService): ICookieHandler
{
    private static readonly string AuthCookieName = Configuration.Cookie.AccessTokenCookieName;

    private static Cookie CreateCookie(string cookieName,string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("O token é vazio.");

        return new Cookie(cookieName, token);
    }

    public async Task SaveAuthToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("O token é vazio.");

        var cookie = CreateCookie(AuthCookieName, token);
        
        await cookieService.SetAsync(cookie);
    }

    public async Task RemoveAuthTokenAsync()
    {
        await cookieService.RemoveAsync(AuthCookieName);
    }

    public async Task<Cookie?> GetAuthToken() => await cookieService.GetAsync(AuthCookieName);
}