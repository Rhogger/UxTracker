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
    public string Image { get; private set; } = string.Empty;

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (
            !string.Equals(
                code.Trim(),
                Password.ResetCode.Trim(),
                StringComparison.CurrentCultureIgnoreCase
            )
        )
            throw new Exception("Código de restauração inválido");

        var password = new Password(plainTextPassword);

        Password = password;
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}
