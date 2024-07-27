using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestSharp;
using UxTracker.Core;

namespace UxTracker.Reviewers.Web.Extensions;

public static class BuilderExtension
{
    public static void AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IRestClient>(sp =>
            {
                var client = new RestClient(Configuration.ApplicationUrl.BackendUrl);
                client.AddDefaultHeader("Accept", "application/json");
                return client;
            }
        );
        
        builder.Services.AddMudServices();
    }
}