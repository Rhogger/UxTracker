using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.NumberCluster, 0, "NumberCluster", "O número de clusters deve ser maior que 0")
            .IsNotNullOrEmpty(request.UserId, "UserId", "Erro ao vincular o usuário");
}