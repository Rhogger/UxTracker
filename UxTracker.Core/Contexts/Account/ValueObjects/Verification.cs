using UxTracker.Core.Contexts.Shared.ValueObjects;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public class Verification : ValueObject
{
    public void Verify(string code)
    {
        if (IsActive)
            throw new Exception("Esse item já foi ativado");

        if (ExpireAt < DateTime.UtcNow)
            throw new Exception("Esse item já expirou");

        if (!IsValid(code))
            throw new Exception("Código de verificação inválido");

        ExpireAt = null;
        VerifiedAt = DateTime.UtcNow;
    }

    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpireAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; }
    public bool IsActive => VerifiedAt != null && ExpireAt == null;
    
    public bool IsValid(string verificationCode) 
        => string.Equals(verificationCode.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase);
}