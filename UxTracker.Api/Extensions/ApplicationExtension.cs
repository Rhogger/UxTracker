namespace UxTracker.Api.Extensions;

public static class ApplicationExtension
{
    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();
    }
}