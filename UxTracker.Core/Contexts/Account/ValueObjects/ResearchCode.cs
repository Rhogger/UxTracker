using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public class ResearchCode: ValueObject
{
    protected ResearchCode(){}
    
    public string Code { get; } = Guid.NewGuid().ToString("N")[..8];
    
    public bool IsValid(string researchCode) 
        => string.Equals(researchCode.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase);

}