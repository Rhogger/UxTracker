using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using UxTracker.Core;

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
        builder.Services.AddBlazoredLocalStorage();
    }
}