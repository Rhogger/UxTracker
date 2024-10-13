using MediatR;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetAll.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.GetAll;

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

        #region 02. Recuperar projetos do banco
        
        List<GetAllDTO>? projects;

        try
        {
            projects = await _repository.GetProjectsByUser(request.UserId, cancellationToken);

            if (projects is null)
                return new Response("Usuário não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion

        #region 03. Retornar os dados
        
        try
        {
            return new Response(string.Empty, new ResponseData(projects));
        }
        catch
        {
            return new Response("Não foi possível retornar os projetos do perfil", 500);
        }

        #endregion
    }
}