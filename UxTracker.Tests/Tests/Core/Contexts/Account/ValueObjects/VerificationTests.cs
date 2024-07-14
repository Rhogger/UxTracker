using UxTracker.Core.Contexts.Account.ValueObjects;
using System.Reflection;

namespace UxTracker.Tests.Core.Contexts.Account.Entities;

[TestClass]

public class VerificationTests
{

  [TestMethod]
  [Description("Dado um código de verificação válido e dentro do período de validade, o método Verify deve definir VerifiedAt e ExpireAt corretamente")]
  public void GivenValidCode_WhenVerifyInvoked_ThenSetsVerifiedAtAndExpireAt()
  {
    // Arrange
    var verification = new Verification();
    var validCode = verification.Code;

    // Act
    verification.Verify(validCode);

    // Assert
    Assert.IsNotNull(verification.VerifiedAt);
    Assert.IsNull(verification.ExpireAt);

  }

  [TestMethod]
  [Description("Dado um código de verificação inválido, o método Verify deve lançar uma exceção")]
  public void GivenInvalidCode_WhenVerifyInvoked_ThenThrowsException()
  {
    // Arrange
    var verification = new Verification();
    var invalidCode = "INVALID";

    // Act & Assert
    Assert.ThrowsException<Exception>(() => verification.Verify(invalidCode), "Código de verificação inválido");
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade IsActive deve retornar false")]
  public void GivenNewInstance_WhenIsActiveInvoked_ThenReturnsFalse()
  {
    // Arrange
    var verification = new Verification();

    // Act

    // Assert
    Assert.IsFalse(verification.IsActive);
  }

  [TestMethod]
  [Description("Dado um item que foi verificado com sucesso, a propriedade IsActive deve retornar true")]
  public void GivenVerifiedInstance_WhenIsActiveInvoked_ThenReturnsTrue()
  {
    // Arrange
    var verification = new Verification();
    verification.Verify(verification.Code);

    // Act

    // Assert
    Assert.IsTrue(verification.IsActive);
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade Code deve conter um código de 6 caracteres em maiúsculas")]
  public void GivenNewInstance_WhenCodeInvoked_ThenReturns6CharacterCode()
  {
    // Arrange
    var verification = new Verification();

    // Assert
    Assert.AreEqual(6, verification.Code.Length);
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade ExpireAt deve ser configurada para 5 minutos no futuro")]
  public void GivenNewInstance_WhenExpireAtInvoked_ThenReturns5MinutesInTheFuture()
  {
    // Arrange
    var verification = new Verification();

    // Act

    // Assert
    Assert.IsNotNull(verification.ExpireAt); // Verifica se ExpireAt não é nulo
    Assert.IsTrue(verification.ExpireAt > DateTime.UtcNow); // Verifica se ExpireAt é configurado para o futuro
    Assert.IsTrue(verification.ExpireAt <= DateTime.UtcNow.AddMinutes(5)); // Verifica se ExpireAt está dentro de 5 minutos
  }

  [TestMethod]
  [Description("Dado um item recém-criado, a propriedade VerifiedAt deve ser nula")]
  public void GivenNewInstance_WhenVerifiedAtInvoked_ThenReturnsNull()
  {
    // Arrange
    var verification = new Verification();

    // Assert
    Assert.IsNull(verification.VerifiedAt);

  }
}

