using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Create.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.Create;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken) =>
        await _context
            .Users
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken) =>
        await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
    
    public async Task AddUserRoleAsync(Guid roleId, Guid userId, CancellationToken cancellationToken)
    {
        FormattableString sql = $"INSERT INTO UserRole (RoleId, UserId) VALUES ({roleId},{userId})";
        await _context.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
    }

    
    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}