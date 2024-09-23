using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class Role: Entity
{
    protected Role() { }

    public Role(string name)
    {
        Name = name;
    }
    public string Name { get; private set; } = string.Empty;
    [JsonIgnore]
    public List<User> Users { get; init; } = new();
}