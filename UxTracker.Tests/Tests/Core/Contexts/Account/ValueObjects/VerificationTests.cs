using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Tests.Core.Contexts.Account.Entities;

[TestClass]

public class VerificationTests
{

  [TestMethod]
  [Description("Dado um código de verificação válido e dentro do período de validade, o método Verify deve definir VerifiedAt e ExpireAt corretamente")]
  public void GivenValidCode_WhenVerifyInvoked_ThenSetsVerifiedAtAndExpireAt()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um código de verificação inválido, o método Verify deve lançar uma exceção")]
  public void GivenInvalidCode_WhenVerifyInvoked_ThenThrowsException()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um código de verificação válido, mas fora do período de validade, o método Verify deve lançar uma exceção")]
  public void GivenValidCodeButExpired_WhenVerifyInvoked_ThenThrowsException()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade IsActive deve retornar false")]
  public void GivenNewInstance_WhenIsActiveInvoked_ThenReturnsFalse()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um item que foi verificado com sucesso, a propriedade IsActive deve retornar true")]
  public void GivenVerifiedInstance_WhenIsActiveInvoked_ThenReturnsTrue()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade Code deve conter um código de 6 caracteres em maiúsculas")]
  public void GivenNewInstance_WhenCodeInvoked_ThenReturns6CharacterCode()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade ExpireAt deve ser configurada para 5 minutos no futuro")]
  public void GivenNewInstance_WhenExpireAtInvoked_ThenReturns5MinutesInTheFuture()
  {

    Assert.Fail();
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade VerifiedAt deve ser nula")]
  public void GivenNewInstance_WhenVerifiedAtInvoked_ThenReturnsNull()
  {

    Assert.Fail();
  }
}

