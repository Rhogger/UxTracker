using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.Entities;
    
[TestClass]
public class ResearcherTests
{
    [TestMethod]
    public void Researcher_Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        const string? name = "John Doe";
        var email = new Email("researcher@example.com");
        var password = new Password("Password123!");

        // Act
        var researcher = new Researcher(name, email, password);

        // Assert
        Assert.AreEqual(name, researcher.Name);
        Assert.AreEqual(email, researcher.Email);
        Assert.AreEqual(password.Hash, researcher.Password?.Hash);
        Assert.IsNotNull(researcher.Projects);
    }

    [TestMethod]
    public void Researcher_UpdateName_ShouldChangeName()
    {
        // Arrange
        var researcher = new Researcher("John Doe", new Email("researcher@example.com"), null);
        const string? newName = "Jane Smith";

        // Act
        researcher.UpdateName(newName);

        // Assert
        Assert.AreEqual(newName, researcher.Name);
    }

    [TestMethod]
    [DataRow("John Doe", "Jane Smith", true)]
    [DataRow("John Doe", "John Doe", false)]
    public void Researcher_IsNewName_ShouldReturnCorrectly(string? currentName, string? newName, bool expectedResult)
    {
        // Arrange
        var researcher = new Researcher(currentName, new Email("researcher@example.com"), null);

        // Act
        var result = researcher.IsNewName(newName);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}