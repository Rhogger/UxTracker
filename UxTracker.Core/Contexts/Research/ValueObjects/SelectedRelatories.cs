namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class SelectedRelatories
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsChecked { get; set; }
}