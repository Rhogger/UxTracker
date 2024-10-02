using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Review.Entities;
 
[TestClass]
public class UserAcceptedTcleTests
{
    [TestMethod]
    public void UserAcceptedTcle_Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var acceptedAt = DateTime.UtcNow;

        // Act
        var userAcceptedTcle = new UserAcceptedTcle(userId, projectId, acceptedAt);

        // Assert
        Assert.AreEqual(userId, userAcceptedTcle.UserId);
        Assert.AreEqual(projectId, userAcceptedTcle.ProjectId);
        Assert.AreEqual(acceptedAt, userAcceptedTcle.AcceptedAt);
    }
}