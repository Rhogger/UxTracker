using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.Entities;

public class Relatory: Entity
{
    public string Title { get; init; } = string.Empty;
    [JsonIgnore]
    public List<Project> Projects { get; init; } = [];
}