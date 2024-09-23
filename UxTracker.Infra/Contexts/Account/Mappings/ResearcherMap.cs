using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Infra.Contexts.Account.Mappings;

public class ResearcherMap : IEntityTypeConfiguration<Researcher>
{
    public void Configure(EntityTypeBuilder<Researcher> builder)
    {
        builder.ToTable("Researchers");

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired(true);
     
        builder
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Researcher>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}