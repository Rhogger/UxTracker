using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class Researcher: User
{
    protected Researcher() { }
    public Researcher(string name, Email email, Password? password): base(email, password)
    {
        Name = name;
    }
    
    public string Name { get; private set; } = string.Empty;
    [JsonIgnore]
    public List<Project> Projects { get; init; } = new();
    
    public void UpdateName(string name) => Name = name;
    
    public bool IsNewName(string name) => !Name.Equals(name);

}