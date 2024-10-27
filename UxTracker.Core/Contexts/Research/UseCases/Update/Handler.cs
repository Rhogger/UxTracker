using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Update.Contracts;
using UxTracker.Core.Contexts.Shared.UseCases;

namespace UxTracker.Core.Contexts.Research.UseCases.Update;

public class Handler : ITransactionalHandler<Request, Response>
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
        
        #region 02. Validar Requisição
        
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

        #region 03. Reidratração do Objeto

        Project? project;
        var isNewFile = false;

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
        
        #region 04. Atualizar o Objeto

        try
        {
            if(project.IsNewTitle(request.Title))
                project.UpdateTitle(request.Title);
            
            if(project.IsNewDescription(request.Description))
                project.UpdateDescription(request.Description);
            
            if(project.IsNewStartDate(request.StartDate))
                project.UpdateStartDate(request.StartDate);
            
            if(project.IsNewEndDate(request.EndDate))
                project.UpdateEndDate(request.EndDate);
            
            if(project.IsNewPeriodType(request.PeriodType))
                project.UpdatePeriodType(request.PeriodType);
            
            if(project.IsNewSurveyCollections(request.SurveyCollections))
                project.UpdateSurveyCollections(request.SurveyCollections);

            if (project.IsNewConsentTerm(request.ConsentTermHash))
            {
                project.UpdateConsentTermHash(request.ConsentTermHash);
                isNewFile = true;
            }

            if (project.IsNewsRelatories(request.Relatories))
            {
                var newRelatories = await _repository.GetRelatoriesByIdAsync(request.Relatories, cancellationToken);
                
                if (newRelatories is null || newRelatories.Count == 0)
                {
                    return new Response("Nenhum relatório foi encontrado", 404);
                }
                
                project.UpdateRelatories(newRelatories);
            }
        }
        catch(Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion
        
        #region 05. Atualizar no banco

        try
        {
            _repository.AttachProject(project);
            _repository.AttachRelatories(project.Relatories);
            await _repository.UpdateProjectAsync(project, cancellationToken);
        }
        catch
        {
            await _repository.RollbackAsync(cancellationToken);
            return new Response("Falha ao atualizar o projeto", 500);
        }

        #endregion

        #region 06. Retornar os dados

        UpdateDTO projectFiltered = new();

        projectFiltered.Title = project.Title;
        projectFiltered.Description = project.Description;
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
        
        
        return new Response("Projeto atualizado com sucesso!", new ResponseData(projectFiltered, isNewFile));

        #endregion
    }
}