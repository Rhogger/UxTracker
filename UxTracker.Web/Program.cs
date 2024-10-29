using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UxTracker.Web;
using UxTracker.Web.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddDataAllocation();
builder.AddServices();
builder.AddSecurity();
builder.AddHandlers();

await builder.Build().RunAsync();