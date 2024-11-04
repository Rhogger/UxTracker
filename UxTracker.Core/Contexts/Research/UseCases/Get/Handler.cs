using MediatR;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.Get.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.Get;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar requisição

        try
        {
            var req = Specification.Ensure(request);

            if (!req.IsValid)
                return new Response("Requisição inválida", 400, req.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Gerar objetos

        var projectUrl = Path.Combine(Configuration.ApplicationUrl.FrontendUrl, "reviewers", "research", request.ProjectId);

        #endregion
        
        #region 03. Recuperar projeto do banco

        GetDto? project;

        try
        {
            project = await repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);

            if (project is null)
                return new Response("Projeto não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o projeto", 500);
        }

        #endregion
        
        #region 04. Retornar os dados

        project.ResearchUrl = projectUrl;

        return new Response(string.Empty, new ResponseData(project));

        #endregion
    }
}