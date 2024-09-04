using MediatR;
using Microsoft.AspNetCore.Authorization;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using CreateInfra = UxTracker.Infra.Contexts.Research.UseCases.Create;

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
    }

    public static void MapResearchEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost(
            "api/v1/projects/create",
            // [Authorize (Policy = "ResearcherPolicy")] 
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
                    ? Results.Created()
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );

        #endregion
    }
}