using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .AreEquals(request.ResetCode.Length, 8, "ResetCode", "Código deve conter 8 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválido");
}