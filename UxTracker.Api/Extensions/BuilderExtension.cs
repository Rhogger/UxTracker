using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        //TODO: Analisar utilidade
        // Configuration.Secrets.ApiKey =
        //     builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") 
        //     ?? string.Empty;
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
    
    public static void AddFormOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 20 * 1024 * 1024;
        });
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
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });

        builder.Services.AddAuthorizationBuilder();
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {        
        builder.Services.AddAntiforgery();
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
        builder.Services.AddTransient<IJwtService, JwtService>();
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
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(n => n.FullName);
            options.SwaggerDoc("v1", 
                new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "UxTracker Api",
                    Description = "Api de toda aplicação do Ux Tracker e seus submódulos"
                }
            );
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "O Header de Autenticação JWT está utilizando o Bearer Scheme. \r\n\r\nDigite 'Bearer' antes de colocar o token."
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}