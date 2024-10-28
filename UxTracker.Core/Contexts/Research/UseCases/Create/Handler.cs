using MediatR;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar Requisição
        
        try
        {
            if (request.Relatories.Any(string.IsNullOrEmpty))
            {
                foreach (var relatory in request.Relatories.ToList())
                {
                    request.Relatories.Remove(relatory);
                }
            }
            
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
            var relatories = await repository.GetRelatoriesByIdAsync(request.Relatories, cancellationToken);

            if (relatories is null || relatories.Count == 0)
            {
                return new Response("Nenhum relatório foi encontrado", 404);
            }
            
            project = new Project(Guid.Parse(request.UserId), request.Title, request.Description, request.StartDate, request.EndDate, request.PeriodType, request.SurveyCollections, request.ConsentTermHash, relatories);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion

        #region 03. Registrar no banco

        try
        {
            repository.AttachRelatories(project.Relatories);
            await repository.SaveAsync(project, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir os dados", 500);
        }

        #endregion

        #region 04. Retornar os dados
        
        return new Response("Projeto criado com sucesso!", new ResponseData(project.Id.ToString()));

        #endregion
    }
}