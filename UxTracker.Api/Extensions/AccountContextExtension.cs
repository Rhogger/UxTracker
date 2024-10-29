using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticateResearcher = UxTracker.Core.Contexts.Account.UseCases.AuthenticateResearcher;
using AuthenticateResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.AuthenticateResearcher;
using AuthenticateReviewer = UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;
using AuthenticateReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.AuthenticateReviewer;
using CreateResearcher = UxTracker.Core.Contexts.Account.UseCases.CreateResearcher;
using CreateResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.CreateResearcher;
using CreateReviewer = UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;
using CreateReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.CreateReviewer;
using DeleteResearcher = UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher;
using DeleteResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.DeleteResearcher;
using DeleteReviewer = UxTracker.Core.Contexts.Account.UseCases.DeleteReviewer;
using DeleteReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.DeleteReviewer;
using GetResearcher = UxTracker.Core.Contexts.Account.UseCases.GetResearcher;
using GetResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.GetResearcher;
using GetReviewer = UxTracker.Core.Contexts.Account.UseCases.GetReviewer;
using GetReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.GetReviewer;
using PasswordRecovery = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryInfra = UxTracker.Infra.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryVerify = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using PasswordRecoveryVerifyInfra = UxTracker.Infra.Contexts.Account.UseCases.PasswordRecoveryVerify;
using RefreshToken = UxTracker.Core.Contexts.Account.UseCases.RefreshToken;
using RefreshTokenInfra = UxTracker.Infra.Contexts.Account.UseCases.RefreshToken;
using ResendResetCode = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;
using ResendResetCodeInfra = UxTracker.Infra.Contexts.Account.UseCases.ResendResetCode;
using ResendVerificationCodeResearcher = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher;
using ResendVerificationCodeResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.ResendVerificationCodeResearcher;
using ResendVerificationCodeReviewer = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer;
using ResendVerificationCodeReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.ResendVerificationCodeReviewer;
using UpdatePassword = UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;
using UpdatePasswordInfra = UxTracker.Infra.Contexts.Account.UseCases.UpdatePassword;
using UpdateResearcher = UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher;
using UpdateResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.UpdateResearcher;
using UpdateReviewer = UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;
using UpdateReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.UpdateReviewer;
using VerifyResearcher = UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher;
using VerifyResearcherInfra = UxTracker.Infra.Contexts.Account.UseCases.VerifyResearcher;
using VerifyReviewer = UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer;
using VerifyReviewerInfra = UxTracker.Infra.Contexts.Account.UseCases.VerifyReviewer;

namespace UxTracker.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region AuthenticateResearcher

        builder.Services.AddTransient<
            AuthenticateResearcher.Contracts.IRepository,
            AuthenticateResearcherInfra.Repository
        >();       
        
        builder.Services.AddTransient<
            AuthenticateResearcher.Contracts.IService,
            AuthenticateResearcherInfra.Service
        >();

        #endregion
        
        #region AuthenticateReviewer

        builder.Services.AddTransient<
            AuthenticateReviewer.Contracts.IRepository,
            AuthenticateReviewerInfra.Repository
        >();       
        
        builder.Services.AddTransient<
            AuthenticateReviewer.Contracts.IService,
            AuthenticateReviewerInfra.Service
        >();

        #endregion
        
        #region CreateResearcher
        builder.Services.AddTransient<
            CreateResearcher.Contracts.IRepository,
            CreateResearcherInfra.Repository
        >();

        builder.Services.AddTransient<
            CreateResearcher.Contracts.IService,
            CreateResearcherInfra.Service
        >();
        #endregion
       
        #region CreateReviewer
        builder.Services.AddTransient<
            CreateReviewer.Contracts.IRepository,
            CreateReviewerInfra.Repository
        >();

        builder.Services.AddTransient<
            CreateReviewer.Contracts.IService,
            CreateReviewerInfra.Service
        >();
        #endregion
        
        #region DeleteResearcher
        builder.Services.AddTransient<
            DeleteResearcher.Contracts.IRepository,
            DeleteResearcherInfra.Repository
        >();
        #endregion
        
        #region DeleteReviewer
        builder.Services.AddTransient<
            DeleteReviewer.Contracts.IRepository,
            DeleteReviewerInfra.Repository
        >();
        #endregion
        
        #region GetResearcher

        builder.Services.AddTransient<
            GetResearcher.Contracts.IRepository,
            GetResearcherInfra.Repository
        >();
        
        #endregion
        
        #region GetReviewer

        builder.Services.AddTransient<
            GetReviewer.Contracts.IRepository,
            GetReviewerInfra.Repository
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
        
        #region RefreshToken

        builder.Services.AddTransient<
            RefreshToken.Contracts.IRepository,
            RefreshTokenInfra.Repository
        >();
        
        builder.Services.AddTransient<
            RefreshToken.Contracts.IService,
            RefreshTokenInfra.Service
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
        
        #region ResendVerificationCodeResearcher

        builder.Services.AddTransient<
            ResendVerificationCodeResearcher.Contracts.IRepository,
            ResendVerificationCodeResearcherInfra.Repository
        >();
        
        builder.Services.AddTransient<
            ResendVerificationCodeResearcher.Contracts.IService,
            ResendVerificationCodeResearcherInfra.Service
        >();

        #endregion
        
        #region ResendVerificationCodeReviewer

        builder.Services.AddTransient<
            ResendVerificationCodeReviewer.Contracts.IRepository,
            ResendVerificationCodeReviewerInfra.Repository
        >();
        
        builder.Services.AddTransient<
            ResendVerificationCodeReviewer.Contracts.IService,
            ResendVerificationCodeReviewerInfra.Service
        >();

        #endregion
        
        #region UpdatePassword

        builder.Services.AddTransient<
            UpdatePassword.Contracts.IRepository,
            UpdatePasswordInfra.Repository
        >();
        #endregion
        
        #region UpdateResearcher

        builder.Services.AddTransient<
            UpdateResearcher.Contracts.IRepository,
            UpdateResearcherInfra.Repository
        >();
        #endregion
        
        #region UpdateReviewer

        builder.Services.AddTransient<
            UpdateReviewer.Contracts.IRepository,
            UpdateReviewerInfra.Repository
        >();
        #endregion
        
        #region VerifyResearcher

        builder.Services.AddTransient<
            VerifyResearcher.Contracts.IRepository,
            VerifyResearcherInfra.Repository
        >();
        
        builder.Services.AddTransient<
            VerifyResearcher.Contracts.IService,
            VerifyResearcherInfra.Service
        >();

        #endregion
        
        #region VerifyReviewer

        builder.Services.AddTransient<
            VerifyReviewer.Contracts.IRepository,
            VerifyReviewerInfra.Repository
        >();
        
        builder.Services.AddTransient<
            VerifyReviewer.Contracts.IService,
            VerifyReviewerInfra.Service
        >();

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region AuthenticateResearcher
        app.MapPost(
            "api/v1/users/researchers/authenticate",
            async (
                AuthenticateResearcher.Request request,
                IRequestHandler<
                    AuthenticateResearcher.Request,
                    AuthenticateResearcher.Response
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
        
        #region AuthenticateReviewer
        app.MapPost(
            "api/v1/users/reviewers/authenticate",
            async (
                AuthenticateReviewer.Request request,
                IRequestHandler<
                    AuthenticateReviewer.Request,
                    AuthenticateReviewer.Response
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
        
        #region CreateResearcher
        app.MapPost(
            "api/v1/users/researchers/create",
            async (
                CreateResearcher.Request request,
                IRequestHandler<
                    CreateResearcher.Request,
                    CreateResearcher.Response
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
        
        #region CreateReviewer
        app.MapPost(
            "api/v1/users/reviewers/create",
            async (
                CreateReviewer.Request request,
                IRequestHandler<
                    CreateReviewer.Request,
                    CreateReviewer.Response
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
        
        #region DeleteResearcher
        app.MapPatch(
            "api/v1/users/researchers/account/inactivate",
            [Authorize (Roles = "Researcher")] async (
                HttpContext httpContext, 
                DeleteResearcher.Request request,
                [FromServices] IRequestHandler<DeleteResearcher.Request, DeleteResearcher.Response> handler
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
        
        #region DeleteReviewer
        app.MapPatch(
            "api/v1/users/reviewers/account/inactivate",
            [Authorize (Roles = "Reviewer")] async (
                HttpContext httpContext, 
                DeleteReviewer.Request request,
                [FromServices] IRequestHandler<DeleteReviewer.Request, DeleteReviewer.Response> handler
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
        
        #region GetResearcher
        app.MapGet(
            "api/v1/users/researchers/account/",
            [Authorize (Roles = "Researcher")] async (
                HttpContext httpContext, 
                [FromServices] IRequestHandler<GetResearcher.Request, GetResearcher.Response> handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;
                    
                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new GetResearcher.Request
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
        
        #region GetReviewer
        app.MapGet(
            "api/v1/users/reviewers/account/",
            [Authorize (Roles = "Reviewer")] async (
                HttpContext httpContext, 
                [FromServices] IRequestHandler<GetReviewer.Request, GetReviewer.Response> handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;
                    
                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new GetReviewer.Request
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
            "api/v1/users/researchers/recover",
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
            "api/v1/users/researchers/recover/verify",
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
        
        #region RefreshToken
        app.MapPost(
            "api/v1/refresh", 
            [Authorize] async (
                HttpContext httpContext, 
                IRequestHandler<RefreshToken.Request, RefreshToken.Response> handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new RefreshToken.Request
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
        
        #region ResendResetCode
        app.MapPatch(
            "api/v1/users/researchers/recover/resend",
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
        
        #region ResendVerificationCodeResearcher
        app.MapPatch(
            "api/v1/users/researchers/verify/resend",
            async (
                ResendVerificationCodeResearcher.Request request,
                IRequestHandler<
                    ResendVerificationCodeResearcher.Request,
                    ResendVerificationCodeResearcher.Response
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
        
        #region ResendVerificationCodeReviewer
        app.MapPatch(
            "api/v1/users/reviewers/verify/resend",
            async (
                ResendVerificationCodeReviewer.Request request,
                IRequestHandler<
                    ResendVerificationCodeReviewer.Request,
                    ResendVerificationCodeReviewer.Response
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
            "api/v1/users/researchers/recover/update",
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
        
        #region UpdateResearcher
        
        app.MapPatch(
                "api/v1/users/researchers/account",
                [Authorize (Roles = "Researcher")] async (
                    HttpContext httpContext,
                    UpdateResearcher.Request request,
                    IRequestHandler<
                        UpdateResearcher.Request,
                        UpdateResearcher.Response
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
        
        #region UpdateReviewer
        
        app.MapPatch(
                "api/v1/users/reviewers/account",
                [Authorize (Roles = "Reviewer")] async (
                    HttpContext httpContext,
                    UpdateReviewer.Request request,
                    IRequestHandler<
                        UpdateReviewer.Request,
                        UpdateReviewer.Response
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
        
        #region VerifyResearcher
        app.MapPatch(
            "api/v1/users/researchers/verify",
            async (
                VerifyResearcher.Request request,
                IRequestHandler<
                    VerifyResearcher.Request,
                    VerifyResearcher.Response
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
        
        #region VerifyReviewer
        app.MapPatch(
            "api/v1/users/reviewers/verify",
            async (
                VerifyReviewer.Request request,
                IRequestHandler<
                    VerifyReviewer.Request,
                    VerifyReviewer.Response
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