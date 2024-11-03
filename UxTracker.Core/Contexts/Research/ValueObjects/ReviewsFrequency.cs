namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class ReviewsFrequency(decimal rate, int count)
{
    public readonly decimal Rate = rate;
    public readonly int Count = count;
}