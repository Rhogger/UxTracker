using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Infra.Contexts.Account.Mappings;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Infra.Contexts.Research.Mappings;
using UxTracker.Infra.Contexts.Review.Mappings;

namespace UxTracker.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Researcher> Researchers { get; init; } = null!;
    public DbSet<Reviewer> Reviewers { get; init; } = null!;
    public DbSet<Role> Roles { get; init; } = null!;
    public DbSet<Project> Projects { get; init; } = null!;
    public DbSet<Relatory> Relatories { get; init; } = null!;    
    public DbSet<UserAcceptedTcle> AcceptedTerms { get; init; } = null!;
    public DbSet<Rate> Reviews { get; init; } = null!;
    
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
    
    public void SeedData()
    {
        if (!Relatories.Any())
            Relatories.AddRange(
                new Relatory { Title = "Visão geral da evolução das avaliações" },
                new Relatory { Title = "Avaliações de cada usuário por período" },
                new Relatory { Title = "Distribuição das avaliações por período" },
                new Relatory { Title = "Frequência das avaliações por período de tempo" },
                new Relatory { Title = "Número adequado de clusters de usuário" },
                new Relatory { Title = "Média da experiência do usuário ao longo do tempo" }
            );
    
        if (!Roles.Any())
            Roles.AddRange(
                new Role("Admin"),
                new Role("Researcher"),
                new Role("Reviewer")
            );
    
        SaveChanges();
    }
}