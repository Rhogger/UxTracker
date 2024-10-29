using System.ComponentModel;

namespace UxTracker.Core.Contexts.Research.Enums;

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