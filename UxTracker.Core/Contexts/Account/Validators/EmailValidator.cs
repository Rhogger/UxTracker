using UxTracker.Core.Contexts.Account.Validations;

namespace UxTracker.Core.Contexts.Account.Validators;

public static class EmailValidator
{
    public static string? Validate(string email)
    {
        var contract = EmailValidation.EnsureEmail(email);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}