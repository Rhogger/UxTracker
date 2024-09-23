using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    public void AttachRoles(List<Role> roles);
    Task ValidateVerificationCodeAsync(Researcher user, CancellationToken cancellationToken);
}