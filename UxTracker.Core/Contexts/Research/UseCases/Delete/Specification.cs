using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.UseCases.Delete;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.UserId, "UserId", "Erro ao vincular o usuário")
            .IsNotNullOrEmpty(request.ProjectId, "ProjectId", "Erro ao buscar o projeto");
}