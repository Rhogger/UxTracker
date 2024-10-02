using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.ResendVerificationCodeResearcher;

public class Repository: IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;
    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Researchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task UpdateVerificationCodeAsync(Researcher user, CancellationToken cancellationToken)
    {
        _context
            .Researchers
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}