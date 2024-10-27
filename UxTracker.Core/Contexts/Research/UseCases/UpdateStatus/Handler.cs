using MediatR;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.UpdateStatus.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateStatus;

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

        #region 02. Reidratração do Objeto

        Project? project;

        try
        {
            project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);

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
            _repository.AttachProject(project);
            _repository.AttachRelatories(project.Relatories);
            await _repository.UpdateProjectAsync(project, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao atualizar o projeto", 500);
        }

        #endregion

        #region 05. Retornar os dados

        GetDTO projectFiltered = new();

        projectFiltered.Title = project.Title;
        projectFiltered.Description = project.Description;
        projectFiltered.Status = project.Status;
        projectFiltered.StartDate = project.StartDate;
        projectFiltered.EndDate = project.EndDate;
        projectFiltered.PeriodType = project.PeriodType;
        projectFiltered.SurveyCollections = project.SurveyCollections;
        foreach (var relatoriesFiltered in project.Relatories.Select(relatory => new GetRelatoriesDTO
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