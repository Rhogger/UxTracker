using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Tests.Core.Contexts.Account.Entities;

[TestClass]
public class PasswordTests
{
  [TestMethod]
  [Description("Dado uma senha nula, quando o construtor é invocado, então lança uma exceção")]
  public void GivenNullEmail_WhenContructorInvocked_ThenThrowsException()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado uma senha com comprimento menor que o mínimo permitido, quando o construtor é invocado, então lança uma exceção")]
  public void GivenPasswordShorterThanMinLength_WhenConstructorInvoked_ThenThrowsException()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado uma senha com comprimento maior que o máximo permitido, quando o construtor é invocado, então lança uma exceção")]
  public void GivenPasswordLongerThanMaxLength_WhenConstructorInvoked_ThenThrowsException()
  {
    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado duas senhas diferentes, quando o hash é gerado, então os hashes devem ser únicos")]
  public void GivenDifferentPasswords_WhenHashGenerated_ThenHashesShouldBeUnique()
  {
    Assert.Fail();
  
  }

  [TestMethod]
  [Description("Dado uma senha válida, quando o hash é gerado, então o hash deve ser gerado com sucesso")]
  public void GivenValidPassword_WhenHashGenerated_ThenHashShouldBeGeneratedSuccessfully()
  {
    Assert.Fail();
  }
}