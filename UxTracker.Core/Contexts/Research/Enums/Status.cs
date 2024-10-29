using System.ComponentModel;

namespace UxTracker.Core.Contexts.Research.Enums;

public enum Status
{
    [Description("not-started")]
    NotStarted,
    [Description("in-progress")]
    InProgress,
    [Description("finished")]
    Finished,
}