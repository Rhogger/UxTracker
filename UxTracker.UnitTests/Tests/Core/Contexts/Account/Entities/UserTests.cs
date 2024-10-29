using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.Entities;

[TestClass]
public class UserTests
{
    private class TestUser(Email email, Password? password) : User(email, password);

    [TestMethod]
    public void User_Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var email = new Email("test@example.com");
        var password = new Password("Password123!");

        // Act
        var user = new TestUser(email, password);

        // Assert
        Assert.AreEqual(email, user.Email);
        Assert.AreEqual(password.Hash, user.Password?.Hash);
        Assert.IsTrue(user.IsActive);
        Assert.IsNotNull(user.Roles);
        Assert.AreEqual(0, user.Roles.Count); 
    }

    [TestMethod]
    public void User_UpdateStatusAccount_ShouldToggleIsActive()
    {
        // Arrange
        var email = new Email("test@example.com");
        var password = new Password("Password123!");
        var user = new TestUser(email, password);

        // Act
        user.UpdateStatusAccount();

        // Assert
        Assert.IsFalse(user.IsActive);

        user.UpdateStatusAccount();

        // Assert
        Assert.IsTrue(user.IsActive);
    }

    [TestMethod]
    public void User_UpdatePassword_ShouldChangePasswordHash()
    {
        // Arrange
        var email = new Email("test@example.com");
        var oldPassword = new Password("OldPassword!");
        const string newPassword = "NewPassword123!";
        var user = new TestUser(email, oldPassword);

        // Act
        user.UpdatePassword(newPassword);

        // Assert
        Assert.IsNotNull(user.Password);
        Assert.AreNotEqual(oldPassword.Hash, user.Password?.Hash);
        Assert.IsTrue(user.Password != null && user.Password.IsValid(newPassword)); 
    }
}