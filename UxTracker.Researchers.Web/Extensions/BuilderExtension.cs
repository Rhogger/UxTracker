using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Security;
using UxTracker.Core.Services;
using UxTracker.Researchers.Web.Handlers;
using UxTracker.Researchers.Web.Security;
using UxTracker.Researchers.Web.Services;

namespace UxTracker.Researchers.Web.Extensions;

public static class BuilderExtension
{
    public static void AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IRestClient>(sp =>
        {
            var client = new RestClient(Configuration.ApplicationUrl.BackendUrl, configureSerialization: s => s.UseNewtonsoftJson());
            client.AddDefaultHeader("Accept", "application/json");
            return client;
        });

        builder.Services.AddMudServices();
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
    }
    
    public static void AddSecurity(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore(config =>
        {
            config.AddPolicy("ResearcherPolicy", policy =>
            {
                policy.RequireClaim("role","Researcher");
            });
            config.AddPolicy("ReviewerPolicy", policy =>
            {
                policy.RequireClaim("role","Reviewer");
            });
            config.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim("role","Admin");
            });
        });
        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddScoped<IIdentityClaimsService, IdentityClaimsService>();
        builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>();
        builder.Services.AddScoped(x =>
            (IBlazorAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());
    }
}