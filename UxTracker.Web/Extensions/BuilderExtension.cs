using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Core.Contexts.Review.Handlers;
using UxTracker.Core.Security;
using UxTracker.Core.Services;
using UxTracker.Web.Handlers;
using UxTracker.Web.Security;
using UxTracker.Web.Services;

namespace UxTracker.Web.Extensions;

public static class BuilderExtension
{
    public static void AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IRestClient>(_ =>
        {
            var client = new RestClient(Configuration.ApplicationUrl.BackendUrl, configureSerialization: s => s.UseNewtonsoftJson());
            client.AddDefaultHeader("Accept", "application/json");
            return client;
        });

        builder.Services.AddMudServices();
        builder.Services.AddTransient<FocusMonitorService>();
    }

    public static void AddDataAllocation(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<ICookieService, CookieService>();
    }

    public static void AddHandlers(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddTransient<ICookieHandler, CookieHandler>();
        builder.Services.AddTransient<IAccountContextHandler, AccountContextHandler>();
        builder.Services.AddTransient<IResearchContextHandler, ResearchContextHandler>();
        builder.Services.AddTransient<IReviewContextHandler, ReviewContextHandler>();
    }
    
    public static void AddSecurity(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<IIdentityClaimsService, IdentityClaimsService>();
        builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>();
        builder.Services.AddScoped(x =>
            (IBlazorAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());
    }
}