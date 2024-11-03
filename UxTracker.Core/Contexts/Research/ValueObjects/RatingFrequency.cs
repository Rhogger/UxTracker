namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class RatingFrequency(string label, int count)
{
    public string Label { get; set; } = label;
    public int Count { get; set; } = count;
}