using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UxTracker.Core;
using UxTracker.Core.Contexts.Shared.UseCases;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using CreateInfra = UxTracker.Infra.Contexts.Research.UseCases.Create;
using Delete = UxTracker.Core.Contexts.Research.UseCases.Delete;
using DeleteInfra = UxTracker.Infra.Contexts.Research.UseCases.Delete;
using Get = UxTracker.Core.Contexts.Research.UseCases.Get;
using GetInfra = UxTracker.Infra.Contexts.Research.UseCases.Get;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;
using GetAllInfra = UxTracker.Infra.Contexts.Research.UseCases.GetAll;
using GetForReview = UxTracker.Core.Contexts.Research.UseCases.GetForReview;
using GetForReviewInfra = UxTracker.Infra.Contexts.Research.UseCases.GetForReview;
using GetRelatories = UxTracker.Core.Contexts.Research.UseCases.GetRelatories;
using GetRelatoriesInfra = UxTracker.Infra.Contexts.Research.UseCases.GetRelatories;
using Update = UxTracker.Core.Contexts.Research.UseCases.Update;
using UpdateInfra = UxTracker.Infra.Contexts.Research.UseCases.Update;
using UpdateStatus = UxTracker.Core.Contexts.Research.UseCases.UpdateStatus;
using UpdateStatusInfra = UxTracker.Infra.Contexts.Research.UseCases.UpdateStatus;

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
        
        #region Delete

        builder.Services.AddTransient<
            Delete.Contracts.IRepository,
            DeleteInfra.Repository
        >();
        
        builder.Services.AddTransient<
            ITransactionalHandler<Delete.Request, Delete.Response>, 
            Delete.Handler
        >();
        
        #endregion
        
        #region Get

        builder.Services.AddTransient<
            Get.Contracts.IRepository,
            GetInfra.Repository
        >();
        
        #endregion
        
        #region GetAll

        builder.Services.AddTransient<
            GetAll.Contracts.IRepository,
            GetAllInfra.Repository
        >();
        
        #endregion
        
        #region GetForReview

        builder.Services.AddTransient<
            GetForReview.Contracts.IRepository,
            GetForReviewInfra.Repository
        >();
        
        #endregion
        
        #region GetRelatories

        builder.Services.AddTransient<
            GetRelatories.Contracts.IRepository,
            GetRelatoriesInfra.Repository
        >();
        
        #endregion
        
        #region Update

        builder.Services.AddTransient<
            Update.Contracts.IRepository,
            UpdateInfra.Repository
        >();
        
        builder.Services.AddTransient<
            ITransactionalHandler<Update.Request, Update.Response>, 
            Update.Handler
        >();
        
        #endregion
        
        #region UpdateStatus

        builder.Services.AddTransient<
            UpdateStatus.Contracts.IRepository,
            UpdateStatusInfra.Repository
        >();
        
        builder.Services.AddTransient<
            IRequestHandler<UpdateStatus.Request, UpdateStatus.Response>, 
            UpdateStatus.Handler
        >();
        
        #endregion
    }

    public static void MapResearchEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost(
            "api/v1/projects",
            [Authorize(Roles = "Researcher")] [Consumes("multipart/form-data")]
            async (
                HttpContext httpContext,
                IFormFile? consentTerm,
                [FromForm] Create.Request request,
                [FromServices] IRequestHandler<
                    Create.Request,
                    Create.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                request.UserId = userId;

                if (consentTerm is not { Length: > 0 })
                {
                    return Results.BadRequest("Nenhum arquivo foi enviado.");
                }

                if (!consentTerm.ContentType.Equals("application/pdf", StringComparison.CurrentCultureIgnoreCase))
                {
                    return Results.BadRequest("Apenas arquivos PDF são aceitos.");
                }

                if (consentTerm.Length > Configuration.ConsentTerm.MaxSize)
                {
                    return Results.BadRequest("O arquivo deve ter no máximo 2MB.");
                }

                string fileHash;

                using (var sha256 = SHA256.Create())
                {
                    await using var stream = consentTerm.OpenReadStream();
                    fileHash = BitConverter.ToString(await sha256.ComputeHashAsync(stream)).Replace("-", "")
                        .ToLowerInvariant();
                }

                request.ConsentTermHash = fileHash;

                var result = await handler.Handle(request, new CancellationToken());
                
                if (!result.IsSuccess)
                {
                    return Results.Json(result, statusCode: result.StatusCode);
                }

                var projectId = result.Data?.Id;

                if (projectId.IsNullOrEmpty())
                {
                    return Results.Problem("Não foi possível identificar o projeto.");
                }

                {
                    var directory = Path.Combine(Configuration.ConsentTerm.Url, projectId);

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var filePath = Path.Combine(directory, $"{consentTerm.FileName}");

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await consentTerm.CopyToAsync(stream);
                }

                return Results.Created("api/v1/projects/create", result);
            }
        ).DisableAntiforgery();

        #endregion

        #region Delete

        app.MapDelete(
            $"api/v1/projects/{{projectId}}",
            [Authorize(Roles = "Researcher")] async (
                HttpContext httpContext,
                [FromRoute] string projectId,
                [FromServices] ITransactionalHandler<Delete.Request, Delete.Response> handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new Delete.Request
                {
                    UserId = userId,
                    ProjectId = projectId,
                };

                var result = await handler.Handle(request, new CancellationToken());

                if (!result.IsSuccess)
                {
                    await handler.RollbackAsync();
                    return Results.Json(result, statusCode: result.StatusCode);
                }

                var directory = $"{Configuration.ConsentTerm.Url}{projectId}";

                try
                {
                    if (!Directory.Exists(directory)) throw new Exception("Diretório não encontrado");

                    Directory.Delete(directory, recursive: true);
                }
                catch (Exception ex)
                {
                    await handler.RollbackAsync();
                    return Results.Problem($"Erro ao tentar deletar o arquivo PDF: {ex.Message}");
                }

                await handler.CommitAsync();

                return Results.Ok(result);
            }
        );

        #endregion

        #region Get

        app.MapGet(
            $"api/v1/projects/{{projectId}}",
            [Authorize(Roles = "Researcher")]
            async (
                HttpContext httpContext,
                [FromRoute] string projectId,
                IRequestHandler<
                    Get.Request,
                    Get.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new Get.Request
                {
                    UserId = userId,
                    ProjectId = projectId,
                };

                var result = await handler.Handle(request, new CancellationToken());
                
                var consentTermFolder = Path.Combine(Configuration.ConsentTerm.Url, projectId);

                if (!Directory.Exists(consentTermFolder))
                    return Results.NotFound("Termo de Aceite não existe");
                
                var files = Directory.GetFiles(consentTermFolder);
                
                if (files.Length == 0)
                    return Results.NotFound("Termo de Aceite não existe");

                var fileName = Path.GetFileName(files[0]);

                var fileUrl = Path.Combine(Configuration.ApplicationUrl.BackendUrl, Configuration.ConsentTerm.Folder, projectId, fileName);

                if (result.Data != null)
                {
                    result.Data.Project.ConsentTermName = fileName;
                    result.Data.Project.ConsentTermUrl = fileUrl;
                }

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );

        #endregion

        #region GetAll

        app.MapGet(
            "api/v1/projects/",
            [Authorize(Roles = "Researcher")]
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

        #region GetConsentTerm

        app.MapGet(
            $"api/v1/term/{{projectId}}",
            async ([FromRoute] string projectId) =>
            {
                var consentTermFolder = Path.Combine(Configuration.ConsentTerm.Url, projectId);

                if (!Directory.Exists(consentTermFolder))
                    return Results.NotFound("Termo de Aceite não existe");
        
                var files = Directory.GetFiles(consentTermFolder);
        
                if (files.Length == 0)
                    return Results.NotFound("Termo de Aceite não existe");

                var filePath = files[0];
                var fileName = Path.GetFileName(filePath);

                var fileContent = await File.ReadAllBytesAsync(filePath);
        
                return Results.File(fileContent, "application/pdf", fileName);
            }
        );

        #endregion
        
        #region GetForReview

        app.MapGet(
            $"api/v1/review/{{projectId}}",
            [Authorize(Roles = "Reviewer")] async (
                HttpContext httpContext,
                [FromRoute] string projectId,
                IRequestHandler<
                    GetForReview.Request,
                    GetForReview.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                var request = new GetForReview.Request
                {
                    UserId = userId,
                    ProjectId = projectId,
                };

                var result = await handler.Handle(request, new CancellationToken());

                if (result.Data is { Accepted: true })
                    return result.IsSuccess 
                        ? Results.Ok(result)
                        : Results.Json(result, statusCode: result.StatusCode);
                
                var consentTermFolder = Path.Combine(Configuration.ConsentTerm.Url, projectId);

                if (!Directory.Exists(consentTermFolder))
                    return Results.NotFound("Termo de Aceite não existe");
                
                var files = Directory.GetFiles(consentTermFolder);
                
                if (files.Length == 0)
                    return Results.NotFound("Termo de Aceite não existe");

                var fileName = Path.GetFileName(files[0]);

                var fileUrl = Path.Combine(Configuration.ApplicationUrl.BackendUrl, Configuration.ConsentTerm.Folder, projectId, fileName);
                
                if (result.Data != null) result.Data = result.Data with { TermUrl = fileUrl };

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );

        #endregion
        
        #region GetRelatories

        app.MapGet(
            $"api/v1/relatories/",
            [Authorize(Roles = "Researcher")]
            async (
                IRequestHandler<
                    GetRelatories.Request,
                    GetRelatories.Response
                > handler
            ) =>
            {
                var result = await handler.Handle(new GetRelatories.Request(), new CancellationToken());

                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.StatusCode);
            }
        );

        #endregion
        
        #region Update

        app.MapPatch(
            $"api/v1/projects/{{projectId}}",
            [Authorize (Roles = "Researcher")]
            [Consumes("multipart/form-data")]
            async (
                HttpContext httpContext,
                IFormFile? consentTerm,
                [FromForm] Update.Request request,
                [FromRoute] string projectId,
                [FromServices] ITransactionalHandler<
                    Update.Request,
                    Update.Response
                > handler
            ) =>
            {
                var userId = httpContext.User.FindFirst("Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();

                request.UserId = userId;

                if (consentTerm is { Length: > 0 })
                {
                    if (!consentTerm.ContentType.Equals("application/pdf", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Results.BadRequest("Apenas arquivos PDF são aceitos.");
                    }
                
                    if (consentTerm.Length > Configuration.ConsentTerm.MaxSize)
                    {
                        return Results.BadRequest("O arquivo deve ter no máximo 2MB.");
                    }

                    string fileHash;

                    using (var sha256 = SHA256.Create())
                    {
                        await using var stream = consentTerm.OpenReadStream();
                        fileHash = BitConverter.ToString(await sha256.ComputeHashAsync(stream)).Replace("-", "").ToLowerInvariant();
                    }

                    request.ConsentTermHash = fileHash;
                }
                
                request.ProjectId = projectId;

                var result = await handler.Handle(request, new CancellationToken());
        
                if (!result.IsSuccess)
                {
                    await handler.RollbackAsync();
                    return Results.Json(result, statusCode: result.StatusCode);
                }

                if (string.IsNullOrEmpty(projectId))
                {
                    await handler.RollbackAsync();
                    return Results.Problem("Não foi possível identificar o projeto.");
                }

                if (!result.Data!.newFile)
                {
                    await handler.CommitAsync();
                    return Results.Ok(result);

                }

                if (consentTerm is { Length: > 0 })
                {
                    var directory = Path.Combine(Configuration.ConsentTerm.Url, projectId);

                    try
                    {
                        if (!Directory.Exists(directory))
                            throw new Exception("Diretório não encontrado");

                        Directory.Delete(directory, recursive: true);
                        Directory.CreateDirectory(directory);

                        var filePath = Path.Combine(directory, $"{consentTerm.FileName}");
                        await using var stream = new FileStream(filePath, FileMode.Create);
                        await consentTerm.CopyToAsync(stream);
                    }
                    catch (Exception ex)
                    {
                        await handler.RollbackAsync();
                        return Results.Problem($"Erro ao tentar atualizar o arquivo PDF: {ex.Message}");
                    }
                }

                await handler.CommitAsync();
                return Results.Ok(result);
            }
        ).DisableAntiforgery();
        
        #endregion
        
        #region UpdateStatus

        app.MapPatch(
            $"api/v1/projects/{{projectId}}/status",
            [Authorize (Roles = "Researcher")]
            async (
                HttpContext httpContext,
                UpdateStatus.Request request,
                [FromRoute] string projectId,
                [FromServices] IRequestHandler<
                    UpdateStatus.Request,
                    UpdateStatus.Response
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