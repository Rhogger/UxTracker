using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Tests.Core.Contexts.Account.Entities;

[TestClass]
public class EmailTests
{
  [TestMethod]
  [Description("Dado um e-mail válido, quando o construtor é invocado, então cria o e-mail")]
  public void GivenValidEmail_WhenConstructorInvoked_ThenCreatesEmail()
  {
    // Arrange
    var validEmail = "user@example.com";

    // Act
    var email = new Email(validEmail);

  }


  [TestMethod]
  [Description("Dado um e-mail inválido, quando o construtor é invocado, então lança exceção")]
  public void GivenInvalidEmail_WhenConstructorInvoked_ThenThrowsException()
  {
    // Assert
    Assert.ThrowsException<Exception>(() => new Email("userexample.com"), "E-mail inválido");
  }


  [TestMethod]
  [Description("Dado um e-mail nulo, quando o construtor é invocado, então lança exceção")]
  public void GivenNullEmail_WhenContructorInvocked_ThenThrowsException()
  {
    //Assert
    Assert.ThrowsException<Exception>(() => new Email(null));
  }

  [TestMethod]
  [Description("Dado um e-mail vazio, quando o construtor é invocado, então lança exceção")]
  public void GivenEmptyEmail_WhenConstructorInvoked_ThenThrowsException()
  {
    //Assert
    Assert.ThrowsException<Exception>(() => new Email(string.Empty));
  }

  [TestMethod]
  [Description("Dado um e-mail com letras maiusculas, quando o construtor é invocado, então o endereco é salvo em letras minusculas")]
  public void GivenEmailWithUpperCase_WhenConstructorInvoked_ThenEmailIsSavedInLowerCase()
  {
    // Arrange
    var validEmail = "USER@Example.Com";
    var expectedEmail = "user@example.com";

    // Act
    var email = new Email(validEmail);

    // Assert
    Assert.AreEqual(expectedEmail, email.Address);
  }


  [TestMethod]
  [Description("Dado um e-mail com letras maiusculas e espaços, quando o construtor é invocado, então o endereço é salvo em letras minusculas e sem espaços")]
  public void GivenEmailWithUpperCaseAndSpaces_WhenConstructorInvoked_ThenEmailIsSavedInLowerCaseAndWithoutSpaces()
  {
    // Arrange
    var emailInput = " User@Example.Com ";
    var expectedEmail = "user@example.com";

    // Act
    var email = new Email(emailInput);

    // Assert
    Assert.AreEqual(expectedEmail, email.Address);
  }
}