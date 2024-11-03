namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class RatingGrouper()
{
    public static List<RatingFrequency> GroupByIntervals(List<decimal> rates)
    {
        var intervals = new List<(decimal min, decimal max, string label)>
        {
            (0.0m, 0.9m, "0-0.9"),
            (1.0m, 1.9m, "1-1.9"),
            (2.0m, 2.9m, "2-2.9"),
            (3.0m, 3.9m, "3-3.9"),
            (4.0m, 4.9m, "4-4.9"),
            (5.0m, 5.9m, "5-5.9"),
            (6.0m, 6.9m, "6-6.9"),
            (7.0m, 7.9m, "7-7.9"),
            (8.0m, 8.9m, "8-8.9"),
            (9.0m, 10.0m, "9-10")
        };

        return intervals.Select(interval =>
            new RatingFrequency(interval.label, rates.Count(rate => rate >= interval.min && rate <= interval.max))
        ).ToList();
    }
}