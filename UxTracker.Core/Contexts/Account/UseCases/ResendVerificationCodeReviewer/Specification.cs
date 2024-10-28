using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido");
}