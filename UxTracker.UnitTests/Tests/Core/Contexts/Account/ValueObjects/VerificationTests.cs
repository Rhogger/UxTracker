using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.ValueObjects
{
    [TestClass]
    public class VerificationTests
    {
        [TestMethod]
        public void Verify_WithCorrectCode_SetsVerifiedAtAndExpiresAtToNull()
        {
            // Arrange
            var verification = new Verification();
            var correctCode = verification.Code;

            // Act
            verification.Verify(correctCode);

            // Assert
            Assert.IsNull(verification.ExpireAt);
            Assert.IsNotNull(verification.VerifiedAt);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Esse item já expirou")]
        public void Verify_WithExpiredCode_ThrowsException()
        {
            // Arrange
            var verification = new Verification();
            verification.GetType().GetProperty("ExpireAt")?.SetValue(verification, DateTime.UtcNow.AddMinutes(-1));

            // Act
            verification.Verify("ANYCODE");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Esse item já foi verificado")]
        public void Verify_WhenAlreadyVerified_ThrowsException()
        {
            // Arrange
            var verification = new Verification();
            verification.Verify(verification.Code);

            // Act
            verification.Verify(verification.Code);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Código de verificação inválido")]
        public void Verify_WithIncorrectCode_ThrowsException()
        {
            // Arrange
            var verification = new Verification();

            // Act
            verification.Verify("WRONGCODE");
        }

        [TestMethod]
        public void IsValid_WithCorrectCode_ReturnsTrue()
        {
            // Arrange
            var verification = new Verification();
            var correctCode = verification.Code;

            // Act
            var result = correctCode != null && verification.IsValid(correctCode);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_WithIncorrectCode_ReturnsFalse()
        {
            // Arrange
            var verification = new Verification();

            // Act
            var result = verification.IsValid("WRONGCODE");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsActive_WhenVerified_ReturnsTrue()
        {
            // Arrange
            var verification = new Verification();
            verification.Verify(verification.Code);

            // Act
            var isActive = verification.IsActive;

            // Assert
            Assert.IsTrue(isActive);
        }

        [TestMethod]
        public void IsActive_WhenNotVerified_ReturnsFalse()
        {
            // Arrange
            var verification = new Verification();

            // Act
            var isActive = verification.IsActive;

            // Assert
            Assert.IsFalse(isActive);
        }
    }
}