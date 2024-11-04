namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class ElbowChartData(int k, decimal wcss)
{
    public int K { get; set; } = k;
    public decimal WCSS { get; set; } = wcss;
}