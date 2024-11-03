namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class ClusterLineData
{
    public string Period { get; set; } = string.Empty; // Ex: "Dia 1", "Dia 2"
    public decimal Value { get; set; } // Média ou valor de avaliação
}