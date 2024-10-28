using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Infra.Contexts.Account.Mappings;

public class ReviewerMap : IEntityTypeConfiguration<Reviewer>
{
    public void Configure(EntityTypeBuilder<Reviewer> builder)
    {
        builder.ToTable("Reviewers");

        builder.Property(x => x.Sex)
            .HasColumnName("Sex")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(x => x.BirthDate)
            .HasColumnName("BirthDate")
            .IsRequired();
        
        builder.Property(x => x.City)
            .HasColumnName("City")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired();
        
        builder.Property(x => x.State)
            .HasColumnName("State")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired();
        
        builder.Property(x => x.Country)
            .HasColumnName("Country")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired();
        
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Reviewer>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}