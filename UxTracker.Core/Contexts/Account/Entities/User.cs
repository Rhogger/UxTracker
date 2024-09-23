using System.Text.Json.Serialization;
using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public abstract class User : Entity
{
    protected User() { }

    protected User(Email email, Password password)
    {
        Email = email;
        Password = password;
        IsActive = true;
    }
    
    public Email Email { get; private set; } = null!;
    public Password? Password { get; private set; }
    public bool IsActive { get; private set; }
    [JsonIgnore]
    public List<Role> Roles { get; init; } = new();


    public void UpdateStatusAccount() => IsActive = !IsActive;
    
    public void UpdatePassword(string plainTextPassword)
    {
        Password = new Password(plainTextPassword);
    }
}