using MediatR;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetForReview.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.GetForReview;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar Requisição
        
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
        
        GetForReviewDTO? project;

        try
        {
            project = await _repository.GetProjectsByIdAsync(request.UserId, request.ProjectId, cancellationToken);

            if (project is null)
                return new Response("Projeto não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion

        #region 03. Verificar se usuário participa da pesquisa

        bool accepted;
        
        try
        {
            accepted = await _repository.IsTermAcceptedAsync(request.UserId, request.ProjectId, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível verificar se o TCLE foi aceito", 500);
        }
        
        #endregion

        #region 04. Retornar os dados
        
        try
        {
            return new Response(string.Empty, new ResponseData(project, accepted));
        }
        catch
        {
            return new Response("Não foi possível retornar os projetos do perfil", 500);
        }

        #endregion
    }
}