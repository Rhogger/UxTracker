using System.ComponentModel;
using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public class Verification : ValueObject
{
    public Verification() { }

    //TODO: Criar Exceptions customizadas
    public void Verify(string code)
    {
        if (IsActive)
            throw new Exception("Esse item já foi ativado");

        if (ExpireAt < DateTime.UtcNow)
            throw new Exception("Esse item já expirou");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de verificação inválido");

        ExpireAt = null;
        VerifiedAt = DateTime.UtcNow;
    }

    public string Code { get; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
    public DateTime? ExpireAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpireAt == null;
}
