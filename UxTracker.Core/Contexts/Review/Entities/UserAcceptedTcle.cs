namespace UxTracker.Core.Contexts.Review.Entities;

public class UserAcceptedTcle
{
    protected UserAcceptedTcle(){}

    public UserAcceptedTcle(Guid userId, Guid projectId, DateTime acceptedAt)
    {
        UserId = userId;
        ProjectId = projectId;
        AcceptedAt = acceptedAt;
    }

    public Guid UserId { get; private set; }
    public Guid ProjectId { get; private set; }
    public DateTime AcceptedAt { get; private set; }
}