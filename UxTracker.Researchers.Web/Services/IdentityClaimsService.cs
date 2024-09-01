using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Services;

namespace UxTracker.Researchers.Web.Services;

public class IdentityClaimsService(ICookieHandler cookieHandler) : IIdentityClaimsService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private static ClaimsPrincipal EmptyClaimsPrincipal => new(new ClaimsIdentity());

    public ClaimsPrincipal BuildClaimsPrincipal(string accessToken)
    {
        if (!ValidateRawToken(accessToken)) return EmptyClaimsPrincipal;

        JwtSecurityToken token;

        try
        {
            token = _tokenHandler.ReadJwtToken(accessToken);
        }
        catch
        {
            return EmptyClaimsPrincipal;
        }
        
        if (!ValidateToken(token)) return EmptyClaimsPrincipal;

        var claims = token.Claims;
        
        claims = claims.Append(new Claim(Configuration.Cookie.AccessTokenCookieName, accessToken));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
    }
    
    private static bool ValidateRawToken(string token) => !string.IsNullOrWhiteSpace(token);

    private static bool ValidateToken(JwtSecurityToken token) => token.ValidTo > DateTime.UtcNow;
}