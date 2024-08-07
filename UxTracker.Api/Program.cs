using UxTracker.Api.Extensions;
using UxTracker.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddServices();
builder.AddDatabase();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddJwtAuthentication();

builder.AddAccountContext();

builder.AddMediator();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSecurity();
app.UseCors(Configuration.Cors.CorsPolicyName);

if (app.Environment.IsDevelopment())
    app.UseConfigurationsDevEnvironment();

app.MapAccountEndpoints();

app.Run();