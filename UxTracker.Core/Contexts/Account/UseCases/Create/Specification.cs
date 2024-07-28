using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.Create;

public static class Specification
{
    //TODO: Ajustar para menos caracteres
    public static Contract<Notification> Ensure(Request request) 
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 80, "Name", "O nome deve conter no máximo 80 caracteres")
            .IsGreaterThan(request.Name.Length, 2, "Name", "O nome deve conter pelo menos 3 caracteres")        
            .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres")
            .IsGreaterThan(request.Password.Length, 7, "Password", "A senha deve conter pelo menos 8 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválido");
    
    public static Contract<Notification> EnsureEmail(string email) 
        => new Contract<Notification>()
            .Requires()
            .IsEmail(email, "Email", "E-mail inválido");

    public static Contract<Notification> EnsureName(string name) 
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(name.Length, 80, "Name", "O nome deve conter no máximo 80 caracteres")
            .IsGreaterThan(name.Length, 2, "Name", "O nome deve conter pelo menos 3 caracteres");

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