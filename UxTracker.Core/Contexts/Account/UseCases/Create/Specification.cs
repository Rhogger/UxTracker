using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request) 
        => new Contract<Notification>()
        .Requires()
        .IsLowerThan(request.Name.Length, 80, "Name", "O nome deve conter no máximo 80 caracteres")
        .IsGreaterThan(request.Name.Length, 3, "Name", "O nome deve conter pelo menos 3 caracteres")        
        .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres")
        .IsGreaterThan(request.Password.Length, 8, "Password", "A senha deve conter pelo menos 8 caracteres")
        .IsEmail(request.Email, "Email", "E-mail inválida");
}
