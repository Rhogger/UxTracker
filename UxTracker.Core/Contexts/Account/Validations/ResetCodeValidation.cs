using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.Validations;

public static class ResetCodeValidation
{
    public static Contract<Notification> EnsureResetCode(string resetCode)
        => new Contract<Notification>()
            .Requires()
            .AreEquals(resetCode.Length, 8, "ResetCode", "Código deve conter 8 caracteres");
}