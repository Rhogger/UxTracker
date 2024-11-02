using UxTracker.Api.Extensions;
using UxTracker.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddServices();
builder.AddDatabase();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddJwtAuthentication();
builder.AddSecurity();

builder.AddFormOptions();
builder.AddAccountContext();
builder.AddResearchContext();
builder.AddReviewContext();

builder.AddMediator();

var app = builder.Build();

app.UseDatabaseManagement();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSecurity();
app.UseCors(Configuration.Cors.CorsPolicyName);

app.MapAccountEndpoints();
app.MapResearchEndpoints();
app.MapReviewEndpoints();

app.Run();