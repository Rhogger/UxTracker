using Microsoft.EntityFrameworkCore;
using UxTracker.Infra.Data;

namespace UxTracker.Api.Extensions;

public static class ApplicationExtension
{
    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();
    }

    public static void UseDatabaseManagement(this IApplicationBuilder app)
    {
        using var serviceScoped = app.ApplicationServices.CreateScope();
        var dbContext = serviceScoped.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}