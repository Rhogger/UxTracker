using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Tests.Core.Contexts.Account.Entities;

[TestClass]
public class EmailTests
{
  [TestMethod]
  [Description("Dado um e-mail válido, quando o construtor é invocado, então cria o e-mail")]
  public void GivenValidEmail_WhenConstructorInvoked_ThenCreatesEmail()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um e-mail inválido, quando o construtor é invocado, então lança exceção")]
  public void GivenInvalidEmail_WhenConstructorInvoked_ThenThrowsException()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um e-mail nulo, quando o construtor é invocado, então lança exceção")]
  public void GivenNullEmail_WhenContructorInvocked_ThenThrowsException()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um e-mail vazio, quando o construtor é invocado, então lança exceção")]
  public void GivenEmptyEmail_WhenConstructorInvoked_ThenThrowsException()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um e-mail com letras maiusculas, quando o construtor é invocado, então o endereco é salvo em letras minusculas")]
  public void GivenEmailWithUpperCase_WhenConstructorInvoked_ThenEmailIsSavedInLowerCase()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um e-mail com letras maiusculas e espaços, quando o construtor é invocado, então o endereco é salvo em letras minusculas e sem espaços")]
  public void GivenEmailWithUpperCaseAndSpaces_WhenConstructorInvoked_ThenEmailIsSavedInLowerCaseAndWithoutSpaces()
  {
    Assert.Fail();
  }
}