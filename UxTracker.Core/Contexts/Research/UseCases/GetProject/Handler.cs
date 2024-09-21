using MediatR;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.GetProject.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.GetProject;

public class Handler: IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }
    
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

        #region 02. Recuperar projeto do banco

        Project? project;

        try
        {
            project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);

            if (project is null)
                return new Response("Projeto não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o projeto", 500);
        }

        #endregion
        
        #region 03. Retornar os dados

        try
        {
            return new Response(string.Empty, new ResponseData(project));
        }
        catch
        {
            return new Response("Não foi possível retornar os dados do perfil", 500);
        }

        #endregion
    }
}