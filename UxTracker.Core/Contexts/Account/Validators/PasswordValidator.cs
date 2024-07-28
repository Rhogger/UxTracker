using UxTracker.Core.Contexts.Account.Validations;

namespace UxTracker.Core.Contexts.Account.Validators;

public static class PasswordValidator
{
    public static string? Validate(string password)
    {
        var contract = PasswordValidation.EnsurePassword(password);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }

    public static string? ComparePasswords(string password, string confirmPassword)
    {
        var contract = PasswordValidation.EnsureComparePasswords(password, confirmPassword);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}