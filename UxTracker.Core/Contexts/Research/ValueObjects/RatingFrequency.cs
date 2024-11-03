namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class RatingFrequency(decimal rate, int count)
{
    public decimal Rate { get; set; } = rate;
    public int Count { get; set; } = count;
}