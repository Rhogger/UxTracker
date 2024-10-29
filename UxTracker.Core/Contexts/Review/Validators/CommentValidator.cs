using UxTracker.Core.Contexts.Review.Validations;

namespace UxTracker.Core.Contexts.Review.Validators;

public static class CommentValidator
{
    public static string? Validate(string comment)
    {
        var contract = CommentValidation.EnsureComment(comment);

        if (contract.IsValid)
            return null;

        var error = contract.Notifications.FirstOrDefault()?.Message;
        return error;
    }
}