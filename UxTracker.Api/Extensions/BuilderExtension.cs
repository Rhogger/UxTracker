using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UxTracker.Core;
using UxTracker.Core.Services;
using UxTracker.Infra.Services;
using UxTracker.Infra.Data;

namespace UxTracker.Api.Extensions;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        Configuration.Secrets.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") 
            ?? string.Empty;
        Configuration.Secrets.JwtPrivateKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey")
            ?? string.Empty;
        Configuration.Secrets.PasswordSaltKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey")
            ?? string.Empty;

        Configuration.SendGrid.ApiKey =
            builder.Configuration.GetSection("SendGrid").GetValue<string>("ApiKey") 
            ?? string.Empty;

        Configuration.Email.DefaultFromEmail =
            builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromEmail")
            ?? string.Empty;
        Configuration.Email.DefaultFromName =
            builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromName")
            ?? string.Empty;

        Configuration.ApplicationUrl.BackendUrl =
            builder.Configuration.GetSection("ApplicationUrl").GetValue<string>("BackendUrl")
            ?? string.Empty;
        Configuration.ApplicationUrl.FrontendUrl =
            builder.Configuration.GetSection("ApplicationUrl").GetValue<string>("FrontendUrl")
            ?? string.Empty;
        
        Configuration.Cors.CorsPolicyName =
            builder.Configuration.GetSection("Cors").GetValue<string>("CorsPolicyName")
            ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(
                Configuration.Database.ConnectionString,
                b => b.MigrationsAssembly("UxTracker.Api")
            )
        );
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization();
    }

    public static void AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(typeof(Configuration).Assembly)
        );
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ISendGridService, SendGridService>();
    }
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                Configuration.Cors.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.ApplicationUrl.BackendUrl,
                        Configuration.ApplicationUrl.FrontendUrl
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }
}