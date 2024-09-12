using UxTracker.Core.Contexts.Research.Validations;

namespace UxTracker.Core.Contexts.Research.Validators;

public static class TitleValidator
{
    public static string? Validate(string title)
    {
        var contract = TitleValidation.EnsureTitle(title);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}