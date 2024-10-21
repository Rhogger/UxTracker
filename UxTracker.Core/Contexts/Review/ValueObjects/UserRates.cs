using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Review.ValueObjects;

public class UserRates: ValueObject
{
    public int Index { get; set; }
    public decimal Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime RatedAt { get; set; }
    
    public bool ValidToRate(PeriodType? periodType, DateTime lastRate)
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
    
    public DateTime GetComingSoonDate(PeriodType? periodType, DateTime lastRate)
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

        return limitDate;
    }
}