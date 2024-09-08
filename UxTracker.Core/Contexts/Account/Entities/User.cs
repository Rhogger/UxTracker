using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class User : Entity
{
    protected User() { }

    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
        IsActive = true;
    }
    
    public User(string name, Email email, Password password, List<Role> roles)
    {
        Name = name;
        Email = email;
        Password = password;
        Roles = roles;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public bool IsActive { get; private set; } = false;
    [JsonIgnore]
    public List<Role> Roles { get; init; } = new();
    [JsonIgnore]
    public List<Project> Projects { get; init; } = new();

    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void UpdatePassword(string plainTextPassword)
    {
        Password = new Password(plainTextPassword);
    }

    public void UpdateStatusAccount() => IsActive = !IsActive;
}