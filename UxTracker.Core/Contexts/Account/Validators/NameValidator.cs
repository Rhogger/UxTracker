using UxTracker.Core.Contexts.Account.Validations;

namespace UxTracker.Core.Contexts.Account.Validators;

public static class NameValidator
{
    public static string? ValidateName(string name)
    {
        var contract = NameValidation.EnsureName(name);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}