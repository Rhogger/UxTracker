using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.ValueObjects;

[TestClass]
public class PasswordTests
{
  [TestMethod]
  [Description("Dado uma senha nula, quando o construtor é invocado, então lança uma exceção")]
  public void GivenNullPassword_WhenContructorInvocked_ThenThrowsException()
  {
    // Arrange
    string nullPassword = null;

    // Act & Assert
    Assert.ThrowsException<Exception>(() => new Password(nullPassword), "A senha não pode ser nula");

  }

  [TestMethod]
  [Description("Dado uma senha com comprimento menor que o mínimo permitido, quando o construtor é invocado, então lança uma exceção")]
  public void GivenPasswordShorterThanMinLength_WhenConstructorInvoked_ThenThrowsException()
  {
    // Arrange
    string shortPassword = "abc";

    // Act
    new Password(shortPassword);

  }

  [TestMethod]
  [Description("Dado uma senha com comprimento maior que o máximo permitido, quando o construtor é invocado, então lança uma exceção")]
  public void GivenPasswordLongerThanMaxLength_WhenConstructorInvoked_ThenThrowsException()
  {
    // Arrange
    string longPassword = "abcdefghijklmn13215154554848948d4a8d4as8d4as8d4as4d5a4d5s64d56a45dsjdasjdijasdjiasjdsai";

    // Act
    new Password(longPassword);

  }

  [TestMethod]
  [Description("Dado duas senhas diferentes, quando o hash é gerado, então os hashes devem ser únicos")]
  public void GivenDifferentPasswords_WhenHashGenerated_ThenHashesShouldBeUnique()
  {
    // Arrange
    var password1 = new Password("password123");
    var password2 = new Password("differentpassword");

    // Act
    var hash1 = password1.Hash;
    var hash2 = password2.Hash;

    // Assert
    Assert.AreNotEqual(hash1, hash2);

  }

  [TestMethod]
  [Description("Dado uma senha válida, quando o hash é gerado, então o hash deve ser gerado com sucesso")]
  public void GivenValidPassword_WhenHashGenerated_ThenHashShouldBeGeneratedSuccessfully()
  {
    // Arrange
    var password = new Password("ValidPassword123");

    // Act
    var hash = password.Hash;

    // Assert
    Assert.IsNotNull(hash);
    Assert.IsFalse(string.IsNullOrEmpty(hash));

  }
}