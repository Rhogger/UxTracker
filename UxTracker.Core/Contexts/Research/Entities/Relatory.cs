using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.Entities;

public class Relatory: Entity
{
    public string Title { get; set; } = string.Empty;
    public List<Project> Projects { get; set; } = new();
}