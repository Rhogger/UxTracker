using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.Validations;

public static class PasswordValidation
{
    public static Contract<Notification> EnsurePassword(string password) 
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres")
            .IsGreaterThan(password.Length, 7, "Password", "A senha deve conter pelo menos 8 caracteres");
    
    public static Contract<Notification> EnsureComparePasswords(string password, string confirmPassword) 
        => new Contract<Notification>()
            .Requires()
            .AreEquals(password,confirmPassword, "Password", "As senhas não coincidem");
}