using UxTracker.Core.Contexts.Research.Validations;

namespace UxTracker.Core.Contexts.Research.Validators;

public static class RelatoriesValidator
{
    public static string? Validate(List<string> ids)
    {
        var contract = RelatoriesValidation.EnsureRelatories(ids);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}