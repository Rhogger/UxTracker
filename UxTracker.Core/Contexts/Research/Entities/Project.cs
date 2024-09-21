using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.Entities;

public class Project: Entity
{
    protected Project() { }

    public Project(Guid userId, string title, string description, DateTime startDate, PeriodType periodType,int surveyCollections, string consentTermHash, List<Relatory> relatories)
    {
        UserId = userId;
        Title = title;
        Description = description;
        StartDate = startDate;
        Status = SetStatus();
        PeriodType = periodType;
        SurveyCollections = surveyCollections;
        LastSurveyCollection = 0;
        ConsentTermHash = consentTermHash;
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
    public int LastSurveyCollection { get;}
    public string ConsentTermHash { get; private set; }
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
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar o título após o fim da pesquisa");
        
        Title = title;
    }
    
    public void UpdateDescription(string description)
    {
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar a descrição após o fim da pesquisa");
        
        Description = description;
    }
    
    public void UpdateStartDate(DateTime date)
    {
        if(IsInvalidToUpdateWhenFinishedStatus )
            throw new Exception("Não pode alterar a data inicial após o fim da pesquisa");
                
        if(IsInvalidToUpdateWhenInProgressStatus && IsInvalidToUpdateWhenHaveDeliveries)
            throw new Exception("Não pode alterar a data inicial se já iniciaram as avaliações");
            
        StartDate = date;
    }
    
    public void UpdatePeriodType(PeriodType periodType)
    {
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar o tipo de período após o fim da pesquisa");
        
        if(IsInvalidToUpdateWhenInProgressStatus && IsInvalidToUpdateWhenHaveDeliveries)
            throw new Exception("Não pode alterar o tipo de período se já iniciaram as avaliações");
        
        PeriodType = periodType;
    }
    
    public void UpdateSurveyCollections(int surveyCollections)
    {
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar a quantidade de coleta após o fim da pesquisa");
        
        if(IsInvalidToUpdateWhenInProgressStatus && IsInvalidToUpdateWhenHaveDeliveries)
            throw new Exception("Não pode alterar a quantidade de coletas se já iniciaram as avaliações");

        if (!IsInvalidToUpdateWhenNotStartedStatus && surveyCollections >= LastSurveyCollection)
            throw new Exception("A quantidade de coleta informada não pode ser inferior a quantidade de coletas entregas");

        SurveyCollections = surveyCollections;
    }

    public void UpdateConsentTermHash(string hash)
    {
        ConsentTermHash = hash;
    }
    
    public void UpdateRelatories(List<Relatory> relatories)
    {
        Relatories = relatories;
    }

    private bool IsInvalidToUpdateWhenNotStartedStatus => Status == Status.NotStarted;
    private bool IsInvalidToUpdateWhenInProgressStatus => Status == Status.InProgress;
    private bool IsInvalidToUpdateWhenFinishedStatus => Status == Status.Finished;
    private bool IsInvalidToUpdateWhenHaveDeliveries => LastSurveyCollection > 0;
    private bool IsValidToUpdateWhenDiffRelatoriesLength(List<string> relatories) => !Relatories.Count.Equals(relatories.Count);

    public bool IsNewTitle(string title) => !Title.Equals(title);
    public bool IsNewDescription(string description) => !Description.Equals(description);
    public bool IsNewStartDate(DateTime startDate) => !StartDate.Equals(startDate);
    public bool IsNewPeriodType(PeriodType periodType) => !PeriodType.Equals(periodType);
    public bool IsNewSurveyCollections(int surveyCollections) => !SurveyCollections.Equals(surveyCollections);
    public bool IsNewConsentTerm(string hash) => !ConsentTermHash.Equals(hash);
    public bool IsNewsRelatories(List<string> relatories)
    {
        if (IsValidToUpdateWhenDiffRelatoriesLength(relatories)) return true;
        
        var relatoryIds = Relatories.Select(r => r.Id.ToString()).ToList();
        var allMatch = relatories.All(id => relatoryIds.Contains(id));

        return !allMatch;
    }


}