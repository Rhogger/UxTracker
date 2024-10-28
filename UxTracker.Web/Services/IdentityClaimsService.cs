using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UxTracker.Core;
using UxTracker.Core.Services;

namespace UxTracker.Web.Services;

public class IdentityClaimsService : IIdentityClaimsService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private static ClaimsPrincipal EmptyClaimsPrincipal => new(new ClaimsIdentity());

    public ClaimsPrincipal BuildClaimsPrincipal(string? accessToken)
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

        var claims = token.Claims.ToList();

        var roleClaim = claims.FirstOrDefault(c => c.Type == "role");
        
        if(roleClaim is not null) claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));

        if (Configuration.Cookie.AccessTokenCookieName == null)
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
        if (accessToken != null)
            claims.Add(new Claim(Configuration.Cookie.AccessTokenCookieName, accessToken));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
    }
    
    private static bool ValidateRawToken(string? token) => !string.IsNullOrWhiteSpace(token);

    private static bool ValidateToken(JwtSecurityToken token) => token.ValidTo > DateTime.UtcNow;
}