using System.Text.RegularExpressions;
using UxTracker.Core.Contexts.Shared.Extensions;
using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public partial class Email : ValueObject
{
    protected Email() { }

    //TODO: Criar Exceptions customizadas
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

    public const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    public string Address { get; }
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();

    public void ResendVerification()
    {
        Verification = new Verification();
    }

    public static implicit operator string(Email email) => email.ToString();

    public static implicit operator Email(string address) => new(address);

    public override string ToString() => Address.Trim().ToLower();

    [GeneratedRegex(Pattern)]
    public static partial Regex EmailRegex();
}
