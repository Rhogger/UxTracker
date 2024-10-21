using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Review.Validations;

public static class RateValidation
{
    public static Contract<Notification> EnsureRate(decimal rate)
        => new Contract<Notification>()
            .Requires()
            .IsGreaterOrEqualsThan(rate, 0, "Rating", "A nota deve ser maior ou igual a zero")
            .IsLowerOrEqualsThan(rate, 10, "Rating", "A nota deve ser menor ou igual ou igual a zero");
}