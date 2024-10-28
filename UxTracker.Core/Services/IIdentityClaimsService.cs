using System.Security.Claims;

namespace UxTracker.Core.Services;

public interface IIdentityClaimsService
{
    public ClaimsPrincipal BuildClaimsPrincipal(string? accessToken);
}