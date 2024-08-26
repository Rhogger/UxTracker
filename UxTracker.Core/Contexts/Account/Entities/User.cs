using UxTracker.Core.Contexts.Account.ValueObjects;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Account.Entities;

public class User : Entity
{
    protected User() { }

    public User(string name, Email email, Password password = null!)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public User(string email, string password)
    {
        Email = email;
        Password = new Password(password);
    }

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;

    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void UpdatePassword(string plainTextPassword)
    {
        Password = new Password(plainTextPassword);
    }
}