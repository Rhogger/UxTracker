using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.ResendVerificationCodeReviewer;

public class Repository: IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;
    public async Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Reviewers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task UpdateVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken)
    {
        _context
            .Reviewers
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}