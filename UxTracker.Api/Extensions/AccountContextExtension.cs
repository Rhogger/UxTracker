using MediatR;

namespace UxTracker.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.Create.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.Create.Repository
        >();

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.Create.Contracts.IService,
            Infra.Contexts.Account.UseCases.Create.Service
        >();
        #endregion

        #region Authenticate

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.Authenticate.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.Authenticate.Repository
        >();

        #endregion
        
        #region Verify

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.Verify.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.Verify.Repository
        >();

        #endregion
        
        #region ResendVerificationCode

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.ResendVerificationCode.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.ResendVerificationCode.Repository
        >();
        
        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.ResendVerificationCode.Contracts.IService,
            Infra.Contexts.Account.UseCases.ResendVerificationCode.Service
        >();

        #endregion
        
        #region PasswordRecovery

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.PasswordRecovery.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.PasswordRecovery.Repository
        >();
        
        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.PasswordRecovery.Contracts.IService,
            Infra.Contexts.Account.UseCases.PasswordRecovery.Service
        >();

        #endregion
        
        #region PasswordRecoveryVerify

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.PasswordRecoveryVerify.Repository
        >();

        #endregion
        
        #region ResendResetCode

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.ResendResetCode.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.ResendResetCode.Repository
        >();
        
        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.ResendResetCode.Contracts.IService,
            Infra.Contexts.Account.UseCases.ResendResetCode.Service
        >();

        #endregion
        
        #region UpdatePassword

        builder.Services.AddTransient<
            Core.Contexts.Account.UseCases.UpdatePassword.Contracts.IRepository,
            Infra.Contexts.Account.UseCases.UpdatePassword.Repository
        >();
        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create
        app.MapPost(
            "api/v1/users",
            async (
                Core.Contexts.Account.UseCases.Create.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.Create.Request,
                    Core.Contexts.Account.UseCases.Create.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Created($"/api/v1/users/{result.Data?.Id}", result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion  
        
        #region Authenticate
        app.MapPost(
            "api/v1/authenticate",
            async (
                Core.Contexts.Account.UseCases.Authenticate.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.Authenticate.Request,
                    Core.Contexts.Account.UseCases.Authenticate.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if (!result.IsSuccess)
                    return Results.Json(result, statusCode: result.StatusCode);
                
                if (result.Data is null)
                    return Results.Json(result, statusCode: 500);

                result.Data.Token = JwtExtension.Generate(result.Data);

                return Results.Ok(result);
            }
        );
        #endregion
        
        #region Verify
        app.MapPatch(
            "api/v1/verify",
            async (
                Core.Contexts.Account.UseCases.Verify.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.Verify.Request,
                    Core.Contexts.Account.UseCases.Verify.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
        
        #region ResendVerificationCode
        app.MapPatch(
            "api/v1/resend-verification-code",
            async (
                Core.Contexts.Account.UseCases.ResendVerificationCode.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.ResendVerificationCode.Request,
                    Core.Contexts.Account.UseCases.ResendVerificationCode.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
        
        #region PasswordRecovery
        app.MapPatch(
            "api/v1/password-recover",
            async (
                Core.Contexts.Account.UseCases.PasswordRecovery.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.PasswordRecovery.Request,
                    Core.Contexts.Account.UseCases.PasswordRecovery.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
        
        #region PasswordRecoveryVerify
        app.MapPatch(
            "api/v1/password-recover/verify",
            async (
                Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Request,
                    Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
        
        #region ResendResetCode
        app.MapPatch(
            "api/v1/recover-password/resend-reset-code",
            async (
                Core.Contexts.Account.UseCases.ResendResetCode.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.ResendResetCode.Request,
                    Core.Contexts.Account.UseCases.ResendResetCode.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
        
        #region UpdatePassword
        app.MapPatch(
            "api/v1/account/update-password",
            async (
                Core.Contexts.Account.UseCases.UpdatePassword.Request request,
                IRequestHandler<
                    Core.Contexts.Account.UseCases.UpdatePassword.Request,
                    Core.Contexts.Account.UseCases.UpdatePassword.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
    }
}