using System.ComponentModel;

namespace UxTracker.Core.Contexts.Account.Enums;

public enum ValidatorTypes
{
    [Description("none")]
    None,
    
    [Description("validator")]
    Validator,
    
    [Description("comparator")]
    Comparator,
}