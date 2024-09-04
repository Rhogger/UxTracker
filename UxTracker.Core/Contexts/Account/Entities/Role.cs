using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class Role: Entity
{
    public string Name { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new();
}