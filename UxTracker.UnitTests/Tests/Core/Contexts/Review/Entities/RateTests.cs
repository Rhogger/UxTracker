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
        const int rating = 5;
        const string comment = "Excellent project";

        // Act
        var rate = new Rate(userId, projectId,0, rating, comment);

        // Assert
        Assert.AreEqual(userId, rate.UserId);
        Assert.AreEqual(projectId, rate.ProjectId);
        Assert.AreEqual(rating, rate.Rating);
        Assert.AreEqual(comment, rate.Comment);
    }

    [DataTestMethod]
    [DataRow(PeriodType.Daily, 1)]
    [DataRow(PeriodType.Weekly, 7)]
    [DataRow(PeriodType.Monthly, 30)]
    [DataRow(PeriodType.Yearly, 365)]
    public void ValidToRate_ShouldReturnFalseIfNotEnoughTimePassed(PeriodType periodType, int daysToAdd)
    {
        // Arrange
        var lastRate = DateTime.UtcNow.AddDays(-daysToAdd + 1);

        // Act
        var result = Rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsFalse(result, $"Rating should not be valid before {daysToAdd} days for {periodType}.");
    }


    [DataTestMethod]
    [DataRow(PeriodType.Daily, 1)]
    [DataRow(PeriodType.Weekly, 7)]
    public void ValidToRate_ShouldReturnTrueIfEnoughTimePassed_Daily(PeriodType periodType, int daysToAdd)
    {
        // Arrange
        var lastRate = DateTime.UtcNow.AddDays(-daysToAdd);

        // Act
        var result = Rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsTrue(result, $"Rating should be valid after {daysToAdd} day(s) for {periodType}.");
    }

    [DataTestMethod]
    [DataRow(PeriodType.Monthly, 1)]
    public void ValidToRate_ShouldReturnTrueIfEnoughTimePassed_Monthly(PeriodType periodType, int monthsToAdd)
    {
        // Arrange
        var lastRate = DateTime.UtcNow.AddMonths(-monthsToAdd);

        // Act
        var result = Rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsTrue(result, $"Rating should be valid after {monthsToAdd} month(s) for {periodType}.");
    }

    [DataTestMethod]
    [DataRow(PeriodType.Yearly, 1)]
    public void ValidToRate_ShouldReturnTrueIfEnoughTimePassed_Yearly(PeriodType periodType, int yearsToAdd)
    {
        // Arrange
        var lastRate = DateTime.UtcNow.AddYears(-yearsToAdd);

        // Act
        var result = Rate.ValidToRate(periodType, lastRate);

        // Assert
        Assert.IsTrue(result, $"Rating should be valid after {yearsToAdd} year(s) for {periodType}.");
    }
}