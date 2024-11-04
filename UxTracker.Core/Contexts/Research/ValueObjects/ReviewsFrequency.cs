namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class ReviewsFrequency(int index, List<decimal> rate)
{
    public readonly int Index = index;
    public readonly List<decimal> Rates = rate;
}