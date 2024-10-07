using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.Enums;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.Entities;
 
[TestClass]
public class ReviewerTests
{
    private Reviewer _reviewer;

    [TestInitialize]
    public void Setup()
    {
        var email = new Email("test@example.com");
        var password = new Password("Password123");
        _reviewer = new Reviewer(email, password, Sex.Male, new DateTime(1990, 1, 1), "Country", "State", "City");
    }

    [TestMethod]
    public void Constructor_Should_Initialize_Reviewer()
    {
        Assert.IsNotNull(_reviewer);
        Assert.AreEqual("Country", _reviewer.Country);
        Assert.AreEqual("State", _reviewer.State);
        Assert.AreEqual("City", _reviewer.City);
        Assert.AreEqual(Sex.Male, _reviewer.Sex);
    }

    [TestMethod]
    public void UpdateCountry_Should_Change_Country()
    {
        // Act
        _reviewer.UpdateCountry("New Country");

        // Assert
        Assert.AreEqual("New Country", _reviewer.Country);
    }

    [TestMethod]
    public void UpdateState_Should_Change_State()
    {
        // Act
        _reviewer.UpdateState("New State");

        // Assert
        Assert.AreEqual("New State", _reviewer.State);
    }

    [TestMethod]
    public void UpdateCity_Should_Change_City()
    {
        // Act
        _reviewer.UpdateCity("New City");

        // Assert
        Assert.AreEqual("New City", _reviewer.City);
    }
}