using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Core.Contexts.Shared.Entities;

#pragma warning disable CS0414

namespace UxTracker.Core.Contexts.Research.Entities;

public class Project: Entity
{
    protected Project() { }

    public Project(Guid userId, string? title, string? description, DateTime? startDate, DateTime? endDate, PeriodType periodType,int surveyCollections, string consentTermHash, List<Relatory> relatories)
    {
        UserId = userId;
        Title = title;
        Description = description;
        StartDate = startDate ?? DateTime.UtcNow;
        EndDate = endDate;

        if (endDate <= startDate)
            throw new Exception("Data final não pode ser menor ou igual à data inicial");
        
        PeriodType = periodType;
        SurveyCollections = surveyCollections;
        ConsentTermHash = consentTermHash;
        Relatories = relatories;
    }
    
    public Guid UserId { get; private set; }
    public string? Title { get; private set; } = string.Empty;
    public string? Description {get; private set; }  = string.Empty;

    public Status Status
    {
        get
        {
            if (StartDate >= DateTime.UtcNow)
            {
                return Status.NotStarted;
            }

            return EndDate <= DateTime.UtcNow ? Status.Finished : Status.InProgress;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set {}
    }

    public DateTime? StartDate {get; private set; }
    public DateTime? EndDate {get; private set; }
    public PeriodType PeriodType { get; private set; } = PeriodType.Daily;
    public int SurveyCollections { get; private set; }
    public int LastSurveyCollection
    {
        get
        {
            return Reviews.Count > 0 
                    ? Reviews
                        .GroupBy(x => x.UserId)
                        .Select(x => x.Count())
                        .Max()
                    : 0
                ;
        }
    }
    public string ConsentTermHash { get; private set; } = null!;
    public int ClusterNumber { get; private set; }

    [JsonIgnore]
    public Researcher User { get; } = null!;

    public List<Relatory> Relatories { get; private set; } = [];
    public List<Rate> Reviews { get; } = [];
    
    public void UpdateTitle(string? title)
    {
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar o título após o fim da pesquisa");
        
        Title = title;
    }
    
    public void UpdateDescription(string? description)
    {
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar a descrição após o fim da pesquisa");
        
        Description = description;
    }
    
    public void UpdateStartDate(DateTime? date)
    {
        if(IsInvalidToUpdateWhenFinishedStatus )
            throw new Exception("Não pode alterar a data inicial após o fim da pesquisa");
                
        if(IsInvalidToUpdateWhenInProgressStatus && IsInvalidToUpdateWhenHaveDeliveries)
            throw new Exception("Não pode alterar a data inicial se já iniciaram as avaliações");
            
        StartDate = date;
    }
    
    public void UpdateEndDate(DateTime? endDate, DateTime? startDate)
    {
        if(!IsInvalidToUpdateWhenInProgressStatus && endDate <= startDate)
            throw new Exception("Não pode alterar a data final para menor ou igual a data inicial");
     
        EndDate = endDate;
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

        if (IsInvalidToUpdateWhenInProgressStatus && surveyCollections < LastSurveyCollection)
            throw new Exception("A quantidade de coleta informada não pode ser inferior a quantidade máxima de avaliações submetidas por um participante");

        SurveyCollections = surveyCollections;
    }

    public void UpdateConsentTermHash(string hash)
    {
        if(IsInvalidToUpdateWhenFinishedStatus)
            throw new Exception("Não pode alterar o termo após o fim da pesquisa");
        
        if(IsInvalidToUpdateWhenInProgressStatus && IsInvalidToUpdateWhenHaveDeliveries)
            throw new Exception("Não pode alterar o termo se já iniciaram as avaliações");
        
        ConsentTermHash = hash;
    }

    public void UpdateClusterNumber(int number)
    {
        if (IsInvalidToUpdateWhenFinishedStatus)
        {
            if(number <= 0)
                throw new Exception("O número de clusters não pode ser zero ou menor");
            
            if(number > Reviews.GroupBy(x => x.UserId).Count())
                throw new Exception("O número de clusters não pode ser maior que a quantidade de participantes");

            ClusterNumber = number;
        }
        else
            throw new Exception("Não pode alterar o número de clusters se a pesquisa não finalizou");
    }
    
    public void UpdateRelatories(List<Relatory> relatories)
    {
        Relatories = relatories;
    }

    private bool IsInvalidToUpdateWhenInProgressStatus => Status == Status.InProgress;
    private bool IsInvalidToUpdateWhenFinishedStatus => Status == Status.Finished;
    private bool IsInvalidToUpdateWhenHaveDeliveries => LastSurveyCollection > 0;
    private bool IsValidToUpdateWhenDiffRelatoriesLength(List<string> relatories) => !Relatories.Count.Equals(relatories.Count);

    public bool IsNewTitle(string? title) => Title != null && !Title.Equals(title);
    public bool IsNewDescription(string? description) => Description != null && !Description.Equals(description);
    public bool IsNewPeriodType(PeriodType periodType) => !PeriodType.Equals(periodType);
    public bool IsNewSurveyCollections(int surveyCollections) => !SurveyCollections.Equals(surveyCollections);
    public bool IsNewStartDate(DateTime? startDate) => !StartDate.Equals(startDate);
    public bool IsNewEndDate(DateTime? endDate) => !EndDate.Equals(endDate);
    public bool IsNewConsentTerm(string hash)
    {
        if(!string.IsNullOrEmpty(hash))
            return !ConsentTermHash.Equals(hash);
        
        return false;
    }
    public bool IsNewNumberCluster(int number) => !ClusterNumber.Equals(number);
    public bool IsNewsRelatories(List<string> relatories)
    {
        if (IsValidToUpdateWhenDiffRelatoriesLength(relatories)) return true;
        
        var relatoryIds = Relatories.Select(r => r.Id.ToString()).ToList();
        var allMatch = relatories.All(id => relatoryIds.Contains(id));

        return !allMatch;
    }
}