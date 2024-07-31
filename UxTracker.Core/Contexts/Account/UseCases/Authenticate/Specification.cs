
using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request) 
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres")
            .IsGreaterThan(request.Password.Length, 7, "Password", "A senha deve conter pelo menos 8 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválida");
}
