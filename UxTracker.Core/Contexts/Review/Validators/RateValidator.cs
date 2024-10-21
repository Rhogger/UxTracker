using UxTracker.Core.Contexts.Review.Validations;

namespace UxTracker.Core.Contexts.Review.Validators;

public static class RateValidator
{
    public static string? Validate(decimal rate)
    {
        var contract = RateValidation.EnsureRate(rate);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}