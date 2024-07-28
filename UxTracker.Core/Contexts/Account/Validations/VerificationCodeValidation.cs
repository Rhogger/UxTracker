using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.Validations;

public static class VerificationCodeValidation
{
    public static Contract<Notification> EnsureVerificationCode(string verificationCode)
        => new Contract<Notification>()
            .Requires()
            .AreEquals(verificationCode.Length, 6, "VerificationCode", "Código deve conter 6 caracteres");
}