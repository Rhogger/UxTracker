namespace UxTracker.Core.Contexts.Research.ValueObjects;

public class SelectedRelatories
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsChecked { get; set; }
}