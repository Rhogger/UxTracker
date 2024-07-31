using UxTracker.Core.Contexts.Account.Validations;

namespace UxTracker.Core.Contexts.Account.Validators;

public static class ResetCodeValidator
{
    public static string? Validate(string resetCode)
    {
        var contract = ResetCodeValidation.EnsureResetCode(resetCode);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}