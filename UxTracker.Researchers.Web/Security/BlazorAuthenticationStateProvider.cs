using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Security;
using UxTracker.Core.Services;

namespace UxTracker.Researchers.Web.Security;

public class BlazorAuthenticationStateProvider(
    ICookieHandler cookieHandler,
    IIdentityClaimsService claimsService)
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
        Cookie? authCookie;

        try
        {
            authCookie = await cookieHandler.GetAuthToken();
        }
        catch (Exception)
        {
            return Save(UnauthorizedState);
        }

        if (authCookie is null) return Save(UnauthorizedState);

        if (string.IsNullOrWhiteSpace(authCookie.Value)) return Save(UnauthorizedState);
        
        var principal = claimsService.BuildClaimsPrincipal(authCookie.Value);
        return Save(new AuthenticationState(principal));
    }
    
    public void NotifyAuthenticationStateChanged()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}