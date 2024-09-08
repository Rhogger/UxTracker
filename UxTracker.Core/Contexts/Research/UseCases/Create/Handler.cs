using MediatR;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

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

        #region 02. Gerar os Objetos

        Project project;

        try
        {
            var relatories = await _repository.GetRelatoriesById(request.Relatories, cancellationToken);

            if (relatories is null || relatories.Count == 0)
            {
                return new Response("Nenhum relatório foi encontrado", 404);
            }
            
            project = new Project(Guid.Parse(request.UserId), request.Title, request.Description, request.StartDate, request.EndDate, request.PeriodType, relatories);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion

        #region 03. Registrar no banco

        try
        {
            _repository.AttachRelatories(project.Relatories);
            await _repository.SaveAsync(project, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir os dados", 500);
        }

        #endregion

        #region 04. Retornar os dados
        
        return new Response("Projeto criado com sucesso!", 201);

        #endregion
    }
}