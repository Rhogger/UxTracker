using System.ComponentModel;

namespace UxTracker.Core.Contexts.Account.Enums;

public enum Sex
{
    [Description("male")]
    Male,
    
    [Description("female")]
    Female,
    
    [Description("preferNotSay")]
    PreferNotSay,
}