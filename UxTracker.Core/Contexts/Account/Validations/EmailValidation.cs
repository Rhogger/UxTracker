using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.Validations;

public static class EmailValidation
{
    public static Contract<Notification> EnsureEmail(string email) 
        => new Contract<Notification>()
            .Requires()
            .IsEmail(email, "Email", "E-mail inválido");
}