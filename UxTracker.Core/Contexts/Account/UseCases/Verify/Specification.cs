using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .AreEquals(request.VerificationCode.Length, 6, "VerificationCode", "Código deve conter 6 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválido");
}