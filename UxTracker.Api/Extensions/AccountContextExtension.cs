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
    }
}
