using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.UseCases.GetAll;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.UserId, "UserId", "Erro ao vincular o usuário");
}