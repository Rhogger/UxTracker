using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.Entities;

public class Project: Entity
{
    protected Project() { }

    public Project(Guid userId, string title, string description, DateTime startDate, DateTime endDate, PeriodType periodType, List<Relatory> relatories)
    {
        UserId = userId;
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = SetStatus();
        PeriodType = periodType;
        Period = SetPeriod();
        ConsentTerm = new byte[000111];
        ReviewersCount = 0;
        Relatories = relatories;
    }
    
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description {get; private set; }  = string.Empty;
    public Status Status { get; private set; }
    public DateTime StartDate {get; private set; }
    public DateTime EndDate {get; private set; }
    public PeriodType PeriodType { get; private set; } = PeriodType.Daily;
    public int Period { get; private set; }
    public byte[] ConsentTerm { get; private set; }
    public int ReviewersCount { get; }
    [JsonIgnore]
    public User User { get; private set; }
    public List<Relatory> Relatories { get; private set; } = new();

    private Status SetStatus()
    {
        if(DateTime.UtcNow > EndDate)
            return Status.Finished;
        if(StartDate <= DateTime.UtcNow && DateTime.UtcNow <= EndDate)
            return Status.InProgress;

        return Status.NotStarted;
    }
    
    private int SetPeriod()
    {
        var dateDifference = EndDate - StartDate;
        var totalDays = (int)dateDifference.TotalDays;
        return PeriodType switch
        {
            PeriodType.Daily => totalDays,
            PeriodType.Weekly => totalDays / 7,
            PeriodType.Monthly => totalDays / 30,
            _ => totalDays / 365
        };
    }
    
    public void UpdateTitle(string title)
    {
        Title = title;
    }
    
    public void UpdateDescription(string description)
    {
        Description = description;
    }
    
    public void UpdateStartDate(DateTime date)
    {
        StartDate = date;
    }
    
    public void UpdateEndDate(DateTime date)
    {
        EndDate = date;
    }
    
    public void UpdatePeriodType(PeriodType periodType)
    {
        PeriodType = periodType;
    }
    
    public void UpdatePeriod(int period)
    {
        Period = period;
    }
}