using System.Security.Cryptography;
using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public class Password : ValueObject
{
    protected Password() { }

    // Caso quiser gerar senha
    // public Password(string? password = null)
    // {
    //     if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
    //         password = Generate();
    //
    //     Hash = Hashing(password);
    // }
    
    public Password(string password)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            throw new Exception("A senha não pode ser nula");

        Hash = Hashing(password);
    }

    // Caso quiser usar o Generate
    // private const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    // private const string Special = "!@#$%^&*(){}[];çÇ";

    public string Hash { get; } = string.Empty;
    public string? ResetCode { get; private set; }
    public DateTime? ExpireAt { get; private set; }
    public DateTime? ChangedAt { get; private set; }

    // FIX: REMOVER ESSA FUNCIONALIDADE
    // private static string Generate(
    //     short length = 16,
    //     bool includeSpecialChars = true,
    //     bool upperCase = false
    // )
    // {
    //     var chars = includeSpecialChars ? (Valid + Special) : Valid;
    //     var startRandom = upperCase ? 26 : 0;
    //     var index = 0;
    //     var passwordChars = new char[length];
    //     var random = new Random();
    //
    //     while (index < length)
    //         passwordChars[index++] = chars[random.Next(startRandom, chars.Length)];
    //
    //     return new string(passwordChars);
    // }

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
        => string.Equals(verificationCode.Trim(), ResetCode?.Trim(), StringComparison.CurrentCultureIgnoreCase);

    public void Verify(string code)
    {
        if (ExpireAt < DateTime.UtcNow)
            throw new Exception("Esse código já expirou");

        if (!string.Equals(code.Trim(), ResetCode?.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de recuperação inválido");

        ResetCode = null;
        ExpireAt = null;
        ChangedAt = DateTime.UtcNow;
    }

    public void GenerateResetCode()
    {
        ResetCode = Guid.NewGuid().ToString("N")[..8].ToUpper();
        ExpireAt = DateTime.UtcNow.AddMinutes(5);
    } 
}