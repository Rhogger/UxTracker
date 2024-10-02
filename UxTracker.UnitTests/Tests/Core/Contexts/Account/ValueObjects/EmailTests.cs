using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.UnitTests.Tests.Core.Contexts.Account.ValueObjects;
 
[TestClass]
public class EmailTests
{
    [TestMethod]
    [DataRow("test@example.com")]
    [DataRow("user.name+label@example.co.uk")]
    [DataRow("user_name@subdomain.example.org")]
    public void Email_ValidAddress_ShouldCreateEmail(string address)
    {
        // Arrange & Act
        var email = new Email(address);

        // Assert
        Assert.AreEqual(address.ToLower(), email.Address);
    }

    [TestMethod]
    [DataRow("invalid-email")]
    [DataRow("missing-at-sign.com")]
    [DataRow("@missing-user.com")]
    [DataRow("user@.missing-domain")]
    public void Email_InvalidAddress_ShouldThrowException(string invalidAddress)
    {
        // Arrange & Act & Assert
        Assert.ThrowsException<Exception>(() => new Email(invalidAddress), "E-mail inválido");
    }

    [TestMethod]
    public void Email_ResendVerification_ShouldResetVerification()
    {
        // Arrange
        var email = new Email("test@example.com");
        var originalVerification = email.Verification;

        // Act
        email.ResendVerification();

        // Assert
        Assert.AreNotEqual(originalVerification, email.Verification);
    }

    [TestMethod]
    [DataRow("test@example.com")]
    [DataRow("user@domain.org")]
    public void Email_ImplicitOperator_ShouldConvertToString(string address)
    {
        // Arrange
        Email email = new Email(address);

        // Act
        string emailString = email;

        // Assert
        Assert.AreEqual(address.ToLower(), emailString);
    }

    [TestMethod]
    [DataRow("test@example.com")]
    [DataRow("user@domain.org")]
    public void Email_ImplicitOperator_ShouldConvertFromString(string address)
    {
        // Arrange & Act
        Email email = address;

        // Assert
        Assert.AreEqual(address.ToLower(), email.Address);
    }
    
    [TestMethod]
    public void ToSha256Hash_WithValidEmail_ReturnsCorrectHash()
    {
        // Arrange
        var email = new Email("user@example.com");
        var expectedHash = "b4c9a289323b21a01c3e940f150eb9b8c542587f1abfd8f0e1cc1ffc5e475514";

        // Act
        var hash = email.ToSha256Hash();

        // Assert
        Assert.AreEqual(expectedHash, hash);
    }

    [TestMethod]
    [DataRow("valid.email@example.com")]
    [DataRow("user.name+alias@example.com")]
    [DataRow("user-name@example.co.uk")]
    [DataRow("user_name@sub.example.org")]
    public void Email_Regex_ShouldMatchValidEmails(string validEmail)
    {
        // Arrange & Act
        var email = new Email(validEmail);

        // Assert
        Assert.AreEqual(validEmail.ToLower(), email.Address);
    }

    [TestMethod]
    [DataRow("plainaddress")]
    [DataRow("missing@domain")]
    [DataRow("missing.domain.com")]
    [DataRow("missing@.com")]
    [DataRow("invalid@domain,com")]
    [DataRow("user@domain..com")]
    public void Email_Regex_ShouldThrowExceptionForInvalidEmails(string invalidEmail)
    {
        // Arrange, Act & Assert
        Assert.ThrowsException<Exception>(() => new Email(invalidEmail), "E-mail inválido");
    }
}