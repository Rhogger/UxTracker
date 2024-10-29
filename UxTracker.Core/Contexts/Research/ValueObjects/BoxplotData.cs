using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class BoxplotData: ValueObject
{
    public int Index { get; set; }
    public decimal Max { get; private set; }
    public decimal Min { get; private set; }
    public decimal Q1 { get; private set; }
    public decimal Q2 { get; private set; }
    public decimal Q3 { get; private set; }

    private readonly List<decimal> _ratings;

    public BoxplotData(List<decimal> ratings, int index)
    {
        if (ratings == null || ratings.Count == 0)
            throw new ArgumentException("Não há nenhuma avaliação para dispor no gráfico", nameof(ratings));

        _ratings = ratings;
        Index = index + 1;
        
        CalculateBoxplotStatistics();
    }

    private void CalculateBoxplotStatistics()
    {
        var sortedRatings = _ratings.OrderBy(x => x).ToList();

        Min = sortedRatings.First();
        Max = sortedRatings.Last();

        Q1 = CalculatePercentile(sortedRatings, 25);
        Q2 = CalculatePercentile(sortedRatings, 50);
        Q3 = CalculatePercentile(sortedRatings, 75);
    }

    private static decimal CalculatePercentile(List<decimal> sortedData, int percentile)
    {
        if (percentile is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(percentile), "O percentual deve ser entre 0 e 100");

        var n = sortedData.Count;
        var rank = (percentile / 100.0m) * (n - 1);
        var lowerIndex = (int)Math.Floor(rank);
        var upperIndex = (int)Math.Ceiling(rank);

        if (lowerIndex == upperIndex)
            return sortedData[lowerIndex];

        var weight = rank - lowerIndex;
        return sortedData[lowerIndex] * (1 - weight) + sortedData[upperIndex] * weight;
    }
}