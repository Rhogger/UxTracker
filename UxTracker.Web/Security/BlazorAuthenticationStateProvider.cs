using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Security;
using UxTracker.Core.Services;

namespace UxTracker.Web.Security;

public class BlazorAuthenticationStateProvider(
    ICookieHandler cookieHandler,
    IIdentityClaimsService claimsService,
    IAccountContextHandler accountContextHandler)
    : AuthenticationStateProvider, IBlazorAuthenticationStateProvider
{
    private static AuthenticationState UnauthorizedState => new(new ClaimsPrincipal());

    private AuthenticationState? _authenticationState;

    public AuthenticationState? AuthenticationState => _authenticationState;

    private AuthenticationState Save(AuthenticationState state)
    {
        _authenticationState = state;
        return state;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Cookie? accessCookie;
        Cookie? refreshCookie;

        try
        {
            accessCookie = await cookieHandler.GetAccessToken();
            refreshCookie = await cookieHandler.GetRefreshToken();
        }
        catch (Exception)
        {
            return Save(UnauthorizedState);
        }

        if (accessCookie is not null && !string.IsNullOrWhiteSpace(accessCookie.Value))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(accessCookie.Value) as JwtSecurityToken;

            if (jwtToken is null || jwtToken.ValidTo <= DateTime.UtcNow)
                return await GenerateNewsTokens();

            
            var principal = claimsService.BuildClaimsPrincipal(accessCookie.Value);
            return Save(new AuthenticationState(principal));
        }

        if (refreshCookie is null || string.IsNullOrWhiteSpace(refreshCookie.Value)) return Save(UnauthorizedState);

        return await GenerateNewsTokens();
    }
    
    public void NotifyAuthenticationStateChanged()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private async Task<AuthenticationState> GenerateNewsTokens()
    {
        try
        {
            var tokenResponse = await accountContextHandler.RefreshTokenAsync();

            if (tokenResponse is null || !tokenResponse.IsSuccessful) return Save(UnauthorizedState);
                
            var principal = claimsService.BuildClaimsPrincipal(tokenResponse.Data.Data.AccessToken);

            return Save(new AuthenticationState(principal));

        }
        catch
        {
            return Save(UnauthorizedState);
        }
    } 
}