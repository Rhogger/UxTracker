using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AcceptTerm = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;
using AcceptTermInfra = UxTracker.Infra.Contexts.Review.UseCases.AcceptTerm;
using Rating = UxTracker.Core.Contexts.Review.UseCases.Rating;
using RatingInfra = UxTracker.Infra.Contexts.Review.UseCases.Rating;

namespace UxTracker.Api.Extensions;

public static class ReviewContextExtension
{
    public static void AddReviewContext(this WebApplicationBuilder builder)
    {
        #region AcceptTerm

        builder.Services.AddTransient<
            AcceptTerm.Contracts.IRepository,
            AcceptTermInfra.Repository
        >();
        
        #endregion
        
        #region Rating

        builder.Services.AddTransient<
            Rating.Contracts.IRepository,
            RatingInfra.Repository
        >();
        
        #endregion
    }

    public static void MapReviewEndpoints(this WebApplication app)
    {
        #region AcceptTerm

        app.MapPost(
            "api/v1/review/accept-term",
            [Authorize(Roles = "Reviewer")]
            async (
                HttpContext httpContext,
                AcceptTerm.Request request,
                IRequestHandler<
                    AcceptTerm.Request,
                    AcceptTerm.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                request.UserId = userId;

                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        ).DisableAntiforgery();

        #endregion
        
        #region Rating

        app.MapPost(
            $"api/v1/review/{{projectId}}",
            [Authorize(Roles = "Reviewer")]
            async (
                HttpContext httpContext,
                [FromRoute] string projectId,
                Rating.Request request,
                IRequestHandler<
                    Rating.Request,
                    Rating.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                request.UserId = userId;
                request.ProjectId = projectId;

                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        ).DisableAntiforgery();

        #endregion
    }
}