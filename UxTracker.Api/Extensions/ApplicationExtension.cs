using AspNetCore.Scalar;

namespace UxTracker.Api.Extensions;

public static class ApplicationExtension
{
    public static void UseConfigurationsDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseScalar(opt =>
        {
            opt.UseTheme(Theme.Default);
            opt.RoutePrefix = "api-docs";
        });
    }
    
    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        // app.Use(next => context =>
        // {
        //     if (context.Request.Path.StartsWithSegments("/api"))
        //     {
        //         context.Response.Headers.Append("XSRF-TOKEN", context.Request.Headers["X-XSRF-TOKEN"]);
        //     }
        //     return next(context);
        // });
        //
        // app.UseAntiforgery();
    }
}