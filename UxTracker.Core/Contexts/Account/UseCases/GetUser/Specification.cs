using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.GetUser;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires();
}