using MediatR;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.UpdateStatus.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateStatus;

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
            if(project.IsNewEndDate(request.EndDate))
                project.UpdateEndDate(request.EndDate, request.StartDate);
            
            if(project.IsNewStartDate(request.StartDate))
                project.UpdateStartDate(request.StartDate);
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
            repository.AttachRelatories(project.Relatories);
            await repository.UpdateProjectAsync(project, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao atualizar o projeto", 500);
        }

        #endregion

        #region 05. Retornar os dados

        var  projectFiltered = new GetDto
        {
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            PeriodType = project.PeriodType,
            SurveyCollections = project.SurveyCollections
        };

        foreach (var relatoriesFiltered in project.Relatories.Select(relatory => new GetRelatoriesDto
                 {
                     Id = relatory.Id,
                     Title = relatory.Title,
                 }))
        {
            projectFiltered.Relatories.Add(relatoriesFiltered);
        }
        
        
        return new Response("Projeto atualizado com sucesso!", new ResponseData(projectFiltered));

        #endregion
    }
}