using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.Entities;

[TestClass]
public class RoleTests
{
    [TestMethod]
    public void Role_Constructor_ShouldInitializeName()
    {
        // Arrange
        var roleName = "Admin";

        // Act
        var role = new Role(roleName);

        // Assert
        Assert.AreEqual(roleName, role.Name);
    }

    [TestMethod]
    public void Role_Constructor_ShouldInitializeUsersList()
    {
        // Arrange
        var role = new Role("Admin");

        // Assert
        Assert.IsNotNull(role.Users);
        Assert.AreEqual(0, role.Users.Count);
    }

    [TestMethod]
    public void Role_Name_ShouldBeReadOnlyAfterInitialization()
    {
        // Arrange
        var role = new Role("Admin");

        // Act
        var roleName = role.Name;

        // Assert
        Assert.AreEqual("Admin", roleName);
    }
}