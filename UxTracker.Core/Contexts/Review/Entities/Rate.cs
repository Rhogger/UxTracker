using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Review.Entities;

public class Rate: Entity
{
    protected Rate(){}
    
    public Rate(Guid userId, Guid projectId, decimal rating, string comment)
    {
        UserId = userId;
        ProjectId = projectId;
        Rating = rating;
        Comment = comment;
        RatedAt = DateTime.UtcNow;
    }
    
    public Rate(Guid userId, Guid projectId, decimal rating, string comment, DateTime ratedAt)
    {
        UserId = userId;
        ProjectId = projectId;
        Rating = rating;
        Comment = comment;
        RatedAt = ratedAt;
    }
    
    public Guid UserId { get; private set; }
    public Guid ProjectId { get; private set; }
    public decimal Rating { get; private set; }
    public string Comment { get; private set; } = null!;
    public DateTime RatedAt { get; private set; }
    public Reviewer User { get; } = null!;

    [JsonIgnore]
    public Project Project { get; } = null!;

    public static bool ValidToRate(PeriodType? periodType, DateTime lastRate)
    {
        var limitDate = lastRate;

        limitDate = periodType switch
        {
            PeriodType.Daily => limitDate.AddDays(1),
            PeriodType.Weekly => limitDate.AddDays(7),
            PeriodType.Monthly => limitDate.AddMonths(1),
            PeriodType.Yearly => limitDate.AddYears(1),
            _ => limitDate
        };

        return DateTime.UtcNow > limitDate;
    }
}