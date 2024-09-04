using System.ComponentModel;

namespace UxTracker.Core.Contexts.ResearchContext.Enums;

public enum PeriodType
{
    [Description("daily")]
    Daily,
    [Description("weekly")]
    Weekly,
    [Description("monthly")]
    Monthly,
    [Description("yearly")]
    Yearly,
}