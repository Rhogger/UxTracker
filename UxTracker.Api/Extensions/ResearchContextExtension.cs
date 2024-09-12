using MediatR;
using Microsoft.AspNetCore.Authorization;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using CreateInfra = UxTracker.Infra.Contexts.Research.UseCases.Create;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;
using GetAllInfra = UxTracker.Infra.Contexts.Research.UseCases.GetAll;

namespace UxTracker.Api.Extensions;

public static class ResearchContextExtension
{
    public static void AddResearchContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            Create.Contracts.IRepository,
            CreateInfra.Repository
        >();
        
        #endregion
        
        #region GetAll

        builder.Services.AddTransient<
            GetAll.Contracts.IRepository,
            GetAllInfra.Repository
        >();
        
        #endregion
    }

    public static void MapResearchEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost(
            "api/v1/projects/create",
            [Authorize (Policy = "ResearcherPolicy")] 
            // [Consumes("multipart/form-data")]
            async (
                HttpContext httpContext,
                Create.Request request,
                IRequestHandler<
                    Create.Request,
                    Create.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                request.UserId = userId;
                
                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Created()
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );

        #endregion
        
        #region GetAll

        app.MapGet(
            "api/v1/projects/",
            [Authorize (Policy = "ResearcherPolicy")] 
            async (
                HttpContext httpContext,
                IRequestHandler<
                    GetAll.Request,
                    GetAll.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;
                    
                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new GetAll.Request
                {
                    UserId = userId
                };

                var result = await handler.Handle(request, new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );

        #endregion
    }
}