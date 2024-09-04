using UxTracker.Core.Contexts.ResearchContext.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.Entities;

public class Project: Entity
{
    protected Project() { }
    
    public Project(string title, string description, DateTime startDate, DateTime endDate, int period)
    {
        Title = title;
        Description = description;
        Status = SetStatus();
        StartDate = startDate;
        EndDate = endDate;
        Period = period;
    }

    public Project(string title, string description, DateTime startDate, DateTime endDate, PeriodType periodType, int period)
    {
        Title = title;
        Description = description;
        Status = SetStatus();
        StartDate = startDate;
        EndDate = endDate;
        PeriodType = periodType;
        Period = period;
    }
    
    public string Title { get; private set; } = string.Empty;
    public string Description {get; private set; }  = string.Empty;
    public Status Status { get; private set; }
    public DateTime StartDate {get; private set; }
    public DateTime EndDate {get; private set; }
    public PeriodType PeriodType { get; private set; } = PeriodType.Daily;
    public int Period { get; private set; } 
    // public int ReviewersCount { get; }
    // public string ConsentTerm { get; private set; }
    public List<Relatory> Relatories { get; private set; } = new();

    public void UpdateTitle(string title)
    {
        Title = title;
    }
    
    public void UpdateDescription(string description)
    {
        Description = description;
    }

    private Status SetStatus()
    {
        if(StartDate >= EndDate)
            return Status.Finished;
        if(StartDate <= DateTime.UtcNow && DateTime.UtcNow < EndDate)
            return Status.InProgress;

        return Status.NotStarted;
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