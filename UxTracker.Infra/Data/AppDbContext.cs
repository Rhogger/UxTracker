using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Infra.Contexts.Account.Mappings;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Infra.Contexts.Research.Mappings;
using UxTracker.Infra.Contexts.Review.Mappings;

namespace UxTracker.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Researcher> Researchers { get; set; } = null!;
    public DbSet<Reviewer> Reviewers { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Relatory> Relatories { get; set; } = null!;    
    public DbSet<UserAcceptedTcle> AcceptedTerms { get; set; } = null!;
    public DbSet<Rate> Reviews { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new ResearcherMap());
        modelBuilder.ApplyConfiguration(new ReviewerMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new ProjectMap());
        modelBuilder.ApplyConfiguration(new RelatoryMap());        
        modelBuilder.ApplyConfiguration(new AcceptedTermMap());
        modelBuilder.ApplyConfiguration(new ReviewMap());
    }
}