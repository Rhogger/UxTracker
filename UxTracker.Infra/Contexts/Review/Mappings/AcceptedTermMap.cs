using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.Infra.Contexts.Review.Mappings;

public class AcceptedTermMap: IEntityTypeConfiguration<UserAcceptedTcle>
{
    public void Configure(EntityTypeBuilder<UserAcceptedTcle> builder)
    {
        builder.ToTable("AcceptedTerms");
        
        builder.HasKey(x => new { x.UserId, x.ProjectId });
        
        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier")
            .IsRequired(true);
        
        builder.Property(x => x.ProjectId)
            .HasColumnName("ProjectId")
            .HasColumnType("uniqueidentifier")
            .IsRequired(true);
        
        builder.Property(x => x.AcceptedAt)
            .HasColumnName("AcceptedAt")
            .IsRequired(true);
    }
}