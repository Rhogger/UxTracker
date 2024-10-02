using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Review.Entities;
 
[TestClass]
public class RateTests
{
    [TestMethod]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var rating = 5;
        var comment = "Excellent project";
        var ratedAt = DateTime.UtcNow;

        // Act
        var rate = new Rate(userId, projectId, rating, comment, ratedAt);

        // Assert
        Assert.AreEqual(userId, rate.UserId);
        Assert.AreEqual(projectId, rate.ProjectId);
        Assert.AreEqual(rating, rate.Rating);
        Assert.AreEqual(comment, rate.Comment);
        Assert.AreEqual(ratedAt, rate.RatedAt);
    }

    [DataTestMethod]
    [DataRow(PeriodType.Daily, 1)]
    [DataRow(PeriodType.Weekly, 7)]
    [DataRow(PeriodType.Monthly, 30)]
    [DataRow(PeriodType.Yearly, 365)]
    public void ValidToRate_ShouldReturnFalseIfNotEnoughTimePassed(PeriodType periodType, int daysToAdd)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var lastRate = DateTime.UtcNow.AddDays(-daysToAdd + 1);
        var rate = new Rate(userId, projectId, 5, "Great project", lastRate);

        // Act
        var result = rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsFalse(result, $"Rating should not be valid before {daysToAdd} days for {periodType}.");
    }


    [DataTestMethod]
    [DataRow(PeriodType.Daily, 1)]
    [DataRow(PeriodType.Weekly, 7)]
    public void ValidToRate_ShouldReturnTrueIfEnoughTimePassed_Daily(PeriodType periodType, int daysToAdd)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var lastRate = DateTime.UtcNow.AddDays(-daysToAdd);
        var rate = new Rate(userId, projectId, 5, "Great project", lastRate);

        // Act
        var result = rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsTrue(result, $"Rating should be valid after {daysToAdd} day(s) for {periodType}.");
    }

    [DataTestMethod]
    [DataRow(PeriodType.Monthly, 1)]
    public void ValidToRate_ShouldReturnTrueIfEnoughTimePassed_Monthly(PeriodType periodType, int monthsToAdd)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var lastRate = DateTime.UtcNow.AddMonths(-monthsToAdd);
        var rate = new Rate(userId, projectId, 5, "Great project", lastRate);

        // Act
        var result = rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsTrue(result, $"Rating should be valid after {monthsToAdd} month(s) for {periodType}.");
    }

    [DataTestMethod]
    [DataRow(PeriodType.Yearly, 1)]
    public void ValidToRate_ShouldReturnTrueIfEnoughTimePassed_Yearly(PeriodType periodType, int yearsToAdd)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var lastRate = DateTime.UtcNow.AddYears(-yearsToAdd);
        var rate = new Rate(userId, projectId, 5, "Great project", lastRate);

        // Act
        var result = rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsTrue(result, $"Rating should be valid after {yearsToAdd} year(s) for {periodType}.");
    }
}