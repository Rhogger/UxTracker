using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Infra.Contexts.Research.Mappings;

public class RelatoryMap: IEntityTypeConfiguration<Relatory>
{
    public void Configure(EntityTypeBuilder<Relatory> builder)
    {
        builder.ToTable("Relatories");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired();
    }
}