using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Delete.Contracts;
using UxTracker.Core.Contexts.Shared.UseCases;

namespace UxTracker.Core.Contexts.Research.UseCases.Delete;

public class Handler: ITransactionalHandler<Request, Response>
{
    private readonly IRepository _repository;
    
    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task RollbackAsync() => await _repository.RollbackAsync(new CancellationToken());
    public async Task CommitAsync() => await _repository.CommitAsync(new CancellationToken());
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Gerar uma Transaction

        try
        {
            await _repository.CreateTransactionAsync(cancellationToken);
        }
        catch
        {
            return new Response("Falha interna do servidor", 500);
        }

        #endregion
        
        #region 02. Validar requisição

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

        #region 03. Recuperar projeto do banco

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
        
        #region 04. Deletar usuário

        try
        {
            await _repository.DeleteProjectAsync(project, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível excluir o projeto", 500);
        }

        #endregion

        #region 05. Retornar os dados

        return new Response("Projeto deletado com sucesso", 200);

        #endregion
    }
}