using System.ComponentModel;

namespace UxTracker.Researchers.Web.Enums;

public enum ValidatorTypes
{
    [Description("validator")]
    Validator,
    
    [Description("comparator")]
    Comparator,
}