using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.ValueObjects;

[TestClass]
public class PasswordTests
{
    [TestMethod]
    public void Password_Constructor_ShouldCreateNullHash_WhenPasswordIsNull()
    {
        // Arrange & Act
        var password = new Password(null);

        // Assert
        Assert.IsNull(password.Hash);
    }

    [TestMethod]
    public void Password_Constructor_ShouldHashPassword_WhenPasswordIsNotNull()
    {
        // Arrange
        var plainTextPassword = "Password123!";

        // Act
        var password = new Password(plainTextPassword);

        // Assert
        Assert.IsNotNull(password.Hash);
        Assert.IsTrue(password.Hash.Length > 0);
    }

    [TestMethod]
    public void Password_IsValid_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var plainTextPassword = "Password123!";
        var password = new Password(plainTextPassword);

        // Act
        var result = password.IsValid(plainTextPassword);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Password_IsValid_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        // Arrange
        var plainTextPassword = "Password123!";
        var incorrectPassword = "WrongPassword!";
        var password = new Password(plainTextPassword);

        // Act
        var result = password.IsValid(incorrectPassword);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Password_GenerateResetCode_ShouldGenerateNewCode()
    {
        // Arrange
        var password = new Password("Password123!");

        // Act
        password.GenerateResetCode();

        // Assert
        Assert.IsNotNull(password.ResetCode);
        Assert.AreEqual(8, password.ResetCode.Code.Length);
    }

    [TestMethod]
    public void Password_IsValidResetCode_ShouldReturnTrue_WhenCodeMatches()
    {
        // Arrange
        var password = new Password("Password123!");
        password.GenerateResetCode();
        var validResetCode = password.ResetCode.Code;

        // Act
        var result = password.IsValidResetCode(validResetCode);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Password_IsValidResetCode_ShouldReturnFalse_WhenCodeDoesNotMatch()
    {
        // Arrange
        var password = new Password("Password123!");
        password.GenerateResetCode();
        var invalidResetCode = "INVALID";

        // Act
        var result = password.IsValidResetCode(invalidResetCode);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Password_VerifyPassword_ShouldReturnFalse_WhenHashFormatIsInvalid()
    {
        // Arrange
        var invalidHash = "InvalidHash";
        var password = new Password("Password123!");

        // Act
        var result = password.IsValidResetCode(invalidHash);

        // Assert
        Assert.IsFalse(result);
    }
}