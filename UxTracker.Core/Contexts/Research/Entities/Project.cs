using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.Entities;

public class Project: Entity
{
    protected Project() { }

    public Project(Guid userId, string title, string description, DateTime startDate, PeriodType periodType,int surveyCollections, List<Relatory> relatories)
    {
        UserId = userId;
        Title = title;
        Description = description;
        StartDate = startDate;
        Status = SetStatus();
        PeriodType = periodType;
        SurveyCollections = surveyCollections;
        ConsentTermId = Guid.NewGuid();
        ReviewersCount = 0;
        Relatories = relatories;
    }
    
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description {get; private set; }  = string.Empty;
    public Status Status { get; private set; }
    public DateTime StartDate {get; private set; }
    public DateTime? EndDate {get; private set; }
    public PeriodType PeriodType { get; private set; } = PeriodType.Daily;
    public int SurveyCollections { get; private set; }
    public Guid ConsentTermId { get; private set; }
    public int ReviewersCount { get; }
    [JsonIgnore]
    public User User { get; private set; }
    public List<Relatory> Relatories { get; private set; } = new();

    private Status SetStatus()
    {
        if(DateTime.UtcNow >= EndDate)
            return Status.Finished;
        if(StartDate <= DateTime.UtcNow && DateTime.UtcNow < EndDate)
            return Status.InProgress;

        return Status.NotStarted;
    }
    
    public void UpdateTitle(string title)
    {
        if(!IsValidToUpdateWhenFinishedStatus())
            throw new Exception("Não pode alterar o título após o fim da pesquisa");
        
        Title = title;
    }
    
    public void UpdateDescription(string description)
    {
        if(!IsValidToUpdateWhenFinishedStatus())
            throw new Exception("Não pode alterar a descrição após o fim da pesquisa");
        
        Description = description;
    }
    
    public void UpdateStartDate(DateTime date)
    {
        if(!IsValidToUpdateWhenFinishedStatus())
            throw new Exception("Não pode alterar a data inicial após o fim da pesquisa");

        StartDate = date;
    }
    
    public void UpdatePeriodType(PeriodType periodType)
    {
        if(!IsValidToUpdateWhenNotStartedStatus())
            throw new Exception("Não pode alterar o tipo de período após o inicio da pesquisa");
        
        PeriodType = periodType;
    }
    
    public void UpdateSurveyCollections(int surveyCollections)
    {
        if(!IsValidToUpdateWhenNotStartedStatus())
            throw new Exception("Não pode alterar a quantidade de coleta após o inicio da pesquisa");

        SurveyCollections = surveyCollections;
    }

    public void GenerateNewConsentTermId() => ConsentTermId = Guid.NewGuid();

    private bool IsValidToUpdateWhenNotStartedStatus() => Status == Status.NotStarted;
    private bool IsValidToUpdateWhenInProgressStatus() => Status == Status.InProgress;
    private bool IsValidToUpdateWhenFinishedStatus() => Status == Status.Finished;
}