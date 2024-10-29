using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.Extensions;

public static class PeriodTypeExtension
{
    public static string ToHumanize(this PeriodType status) =>
        status switch
        {
            PeriodType.Daily => "Diário",
            PeriodType.Weekly => "Semanal",
            PeriodType.Monthly => "Mensal",
            PeriodType.Yearly => "Anual",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    
    public static string GetPeriod(this PeriodType status) =>
        status switch
        {
            PeriodType.Daily => "Dias",
            PeriodType.Weekly => "Semanas",
            PeriodType.Monthly => "Meses",
            PeriodType.Yearly => "Anos",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}