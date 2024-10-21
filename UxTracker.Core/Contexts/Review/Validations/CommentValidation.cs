using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Review.Validations;

public static class CommentValidation
{
    public static Contract<Notification> EnsureComment(string comment)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(comment, "Comment", "Justifique sua nota");
}