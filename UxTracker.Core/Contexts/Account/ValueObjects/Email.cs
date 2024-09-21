using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public partial class Email : ValueObject
{
    protected Email() { }

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("E-mail inválido");

        Address = address.Trim().ToLower();

        if (Address.Length < 5)
            throw new Exception("E-mail inválido");

        if (!EmailRegex().IsMatch(Address))
            throw new Exception("E-mail inválido");
    }

    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    public string Address { get; }
    public Verification Verification { get; private set; } = new();
    
    public void ResendVerification() =>
        Verification = new Verification();

    public static implicit operator string(Email email) => email.ToString();

    public static implicit operator Email(string address) => new(address);

    public override string ToString() => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
    
    public string ToSha256Hash()
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(Address));

        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}