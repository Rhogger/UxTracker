using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Authenticate = UxTracker.Core.Contexts.Account.UseCases.Authenticate;
using AuthenticateInfra = UxTracker.Infra.Contexts.Account.UseCases.Authenticate;
using Create = UxTracker.Core.Contexts.Account.UseCases.Create;
using CreateInfra = UxTracker.Infra.Contexts.Account.UseCases.Create;
using GetUser = UxTracker.Core.Contexts.Account.UseCases.GetUser;
using GetUserInfra = UxTracker.Infra.Contexts.Account.UseCases.GetUser;
using PasswordRecovery = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryInfra = UxTracker.Infra.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryVerify = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using PasswordRecoveryVerifyInfra = UxTracker.Infra.Contexts.Account.UseCases.PasswordRecoveryVerify;
using ResendResetCode = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;
using ResendResetCodeInfra = UxTracker.Infra.Contexts.Account.UseCases.ResendResetCode;
using ResendVerificationCode = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;
using ResendVerificationCodeInfra = UxTracker.Infra.Contexts.Account.UseCases.ResendVerificationCode;
using UpdateAccount = UxTracker.Core.Contexts.Account.UseCases.UpdateAccount;
using UpdateAccountInfra = UxTracker.Infra.Contexts.Account.UseCases.UpdateAccount;
using UpdatePassword = UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;
using UpdatePasswordInfra = UxTracker.Infra.Contexts.Account.UseCases.UpdatePassword;
using Verify = UxTracker.Core.Contexts.Account.UseCases.Verify;
using VerifyInfra = UxTracker.Infra.Contexts.Account.UseCases.Verify;

namespace UxTracker.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Authenticate

        builder.Services.AddTransient<
            Authenticate.Contracts.IRepository,
            AuthenticateInfra.Repository
        >();       
        
        builder.Services.AddTransient<
            Authenticate.Contracts.IService,
            AuthenticateInfra.Service
        >();

        #endregion
        
        #region Create
        builder.Services.AddTransient<
            Create.Contracts.IRepository,
            CreateInfra.Repository
        >();

        builder.Services.AddTransient<
            Create.Contracts.IService,
            CreateInfra.Service
        >();
        #endregion
        
        #region GetUser

        builder.Services.AddTransient<
            GetUser.Contracts.IRepository,
            GetUserInfra.Repository
        >();
        
        #endregion
        
        #region PasswordRecovery

        builder.Services.AddTransient<
            PasswordRecovery.Contracts.IRepository,
            PasswordRecoveryInfra.Repository
        >();
        
        builder.Services.AddTransient<
            PasswordRecovery.Contracts.IService,
            PasswordRecoveryInfra.Service
        >();

        #endregion
        
        #region PasswordRecoveryVerify

        builder.Services.AddTransient<
            PasswordRecoveryVerify.Contracts.IRepository,
            PasswordRecoveryVerifyInfra.Repository
        >();

        #endregion
        
        #region ResendResetCode

        builder.Services.AddTransient<
            ResendResetCode.Contracts.IRepository,
            ResendResetCodeInfra.Repository
        >();
        
        builder.Services.AddTransient<
            ResendResetCode.Contracts.IService,
            ResendResetCodeInfra.Service
        >();

        #endregion
        
        #region ResendVerificationCode

        builder.Services.AddTransient<
            ResendVerificationCode.Contracts.IRepository,
            ResendVerificationCodeInfra.Repository
        >();
        
        builder.Services.AddTransient<
            ResendVerificationCode.Contracts.IService,
            ResendVerificationCodeInfra.Service
        >();

        #endregion
        
        #region UpdateAccount

        builder.Services.AddTransient<
            UpdateAccount.Contracts.IRepository,
            UpdateAccountInfra.Repository
        >();
        #endregion
        
        #region UpdatePassword

        builder.Services.AddTransient<
            UpdatePassword.Contracts.IRepository,
            UpdatePasswordInfra.Repository
        >();
        #endregion
        
        #region Verify

        builder.Services.AddTransient<
            Verify.Contracts.IRepository,
            VerifyInfra.Repository
        >();
        
        builder.Services.AddTransient<
            Verify.Contracts.IService,
            VerifyInfra.Service
        >();

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Authenticate
        app.MapPost(
            "api/v1/users/researchers/authenticate",
            async (
                Authenticate.Request request,
                IRequestHandler<
                    Authenticate.Request,
                    Authenticate.Response
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
        
        #region Create
        app.MapPost(
            "api/v1/users/researchers/create",
            async (
                Create.Request request,
                IRequestHandler<
                    Create.Request,
                    Create.Response
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
        
        #region GetUser
        app.MapGet(
            "api/v1/account/",
            [Authorize] async (
                HttpContext httpContext, 
                [FromServices] IRequestHandler<GetUser.Request, GetUser.Response> handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new GetUser.Request
                {
                    Id = userId
                };

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
                PasswordRecovery.Request request,
                IRequestHandler<
                    PasswordRecovery.Request,
                    PasswordRecovery.Response
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
                PasswordRecoveryVerify.Request request,
                IRequestHandler<
                    PasswordRecoveryVerify.Request,
                    PasswordRecoveryVerify.Response
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
            "api/v1/password-recover/resend-reset-code",
            async (
                ResendResetCode.Request request,
                IRequestHandler<
                    ResendResetCode.Request,
                    ResendResetCode.Response
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
            "api/v1/users/researchers/verify/resend",
            async (
                ResendVerificationCode.Request request,
                IRequestHandler<
                    ResendVerificationCode.Request,
                    ResendVerificationCode.Response
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
        
        #region UpdateAccount
        app.MapPatch(
            "api/v1/account/",
            [Authorize] async (
                HttpContext httpContext,
                UpdateAccount.Request request,
                IRequestHandler<
                    UpdateAccount.Request,
                    UpdateAccount.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                request.Id = userId;
                
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );
        #endregion
        
        #region UpdatePassword
        app.MapPatch(
            "api/v1/password-recover/update-password",
            async (
                UpdatePassword.Request request,
                IRequestHandler<
                    UpdatePassword.Request,
                    UpdatePassword.Response
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
        
        #region Verify
        app.MapPatch(
            "api/v1/users/researchers/verify",
            async (
                Verify.Request request,
                IRequestHandler<
                    Verify.Request,
                    Verify.Response
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