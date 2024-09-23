using System.Security.Cryptography;
using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public class Password : ValueObject
{
    protected Password() { }
    
    public Password(string? password = null)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            Hash = null;
        else
            Hash = Hashing(password);
    }

    public string? Hash { get; } = string.Empty;
    public Verification? ResetCode { get; private set; }

    private static string Hashing(
        string password,
        short saltSize = 16,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.'
    )
    {
        if (string.IsNullOrEmpty(password))
            throw new Exception("A senha não pode ser nula ou vazia");

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            saltSize,
            iterations,
            HashAlgorithmName.SHA256
        );

        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{iterations}{splitChar}{salt}{splitChar}{key}";
    }

    private static bool VerifyPassword(
        string hash,
        string password,
        short keySize = 32,
        int iterations = 10000,
        char splitChar = '.'
    )
    {
        password += Configuration.Secrets.PasswordSaltKey;

        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hashIterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        if (hashIterations != iterations)
            return false;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256
        );

        var keyToCheck = algorithm.GetBytes(keySize);

        return keyToCheck.SequenceEqual(key);
    }

    public bool IsValid(string plainTextPassword)
        => VerifyPassword(Hash, plainTextPassword);
    
    public bool IsValidResetCode(string verificationCode) 
        => string.Equals(verificationCode.Trim(), ResetCode?.Code.Trim(), StringComparison.CurrentCultureIgnoreCase);

    public void GenerateResetCode()
    {
        ResetCode = new Verification
        {
            Code = Guid.NewGuid().ToString("N")[..8].ToUpper()
        };
    } 
}