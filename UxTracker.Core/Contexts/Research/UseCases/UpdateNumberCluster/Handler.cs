using MediatR;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
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

        #region 02. Reidratração do Objeto

        Project? project;

        try
        {
            project = await repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);

            if (project is null)
            {
                return new Response("Nenhum projeto foi encontrado", 404);
            }
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion
        
        #region 03. Atualizar o Objeto

        try
        {
            if(project.IsNewNumberCluster(request.NumberCluster))
                project.UpdateClusterNumber(request.NumberCluster);
            else
                return new Response("O número de clusters deve ser diferente do que já estava", 400);
        }
        catch(Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion
        
        #region 04. Atualizar no banco

        try
        {
            repository.AttachProject(project);
            await repository.UpdateProjectAsync(project, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao atualizar o projeto", 500);
        }

        #endregion

        #region 05. Retornar os dados
        
        return new Response("Número de clusters atualizado com sucesso!", new ResponseData(request.NumberCluster));

        #endregion
    }
}