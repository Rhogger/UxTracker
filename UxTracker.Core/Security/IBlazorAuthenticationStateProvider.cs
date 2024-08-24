using Microsoft.AspNetCore.Components.Authorization;

namespace UxTracker.Core.Security;

public interface IBlazorAuthenticationStateProvider
{
    public Task<AuthenticationState> GetAuthenticationStateAsync();
    public void NotifyAuthenticationStateChanged();

}