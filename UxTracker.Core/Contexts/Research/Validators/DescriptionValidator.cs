using UxTracker.Core.Contexts.Research.Validations;

namespace UxTracker.Core.Contexts.Research.Validators;

public static class DescriptionValidator
{
    public static string? Validate(string description)
    {
        var contract = DescriptionValidation.EnsureDescription(description);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}