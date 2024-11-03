namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class ClusterSeriesData
{
    public int ClusterId { get; set; }
    public List<ClusterLineData> Data { get; set; } = new List<ClusterLineData>();
}