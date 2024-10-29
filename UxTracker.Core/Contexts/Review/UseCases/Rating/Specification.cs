using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Review.UseCases.Rating;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.UserId, "UserId", "Erro ao vincular o usuário")
            .IsNotNullOrEmpty(request.ProjectId, "ProjectId", "Erro ao buscar a pesquisa")
            .IsGreaterOrEqualsThan(request.Rating, 0, "Rating", "A nota deve ser maior ou igual a zero")
            .IsLowerOrEqualsThan(request.Rating, 10, "Rating", "A nota deve ser menor ou igual ou igual a zero")
            .IsNotNullOrEmpty(request.Comment, "Comment", "Justifique sua nota");
}