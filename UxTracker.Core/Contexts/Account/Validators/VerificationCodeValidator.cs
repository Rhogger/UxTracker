using UxTracker.Core.Contexts.Account.Validations;

namespace UxTracker.Core.Contexts.Account.Validators;

public static class VerificationCodeValidator
{
    public static string? Validate(string verificationCode)
    {
        var contract = VerificationCodeValidation.EnsureVerificationCode(verificationCode);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}