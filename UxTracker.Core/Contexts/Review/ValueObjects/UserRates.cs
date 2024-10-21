using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Review.ValueObjects;

public class UserRates: ValueObject
{
    public int Index { get; set; }
    public decimal Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime RatedAt { get; set; }
}