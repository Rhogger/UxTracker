using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.Infra.Contexts.Review.Mappings;

public class ReviewMap: IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Rating)
            .HasColumnName("Rating")
            .HasColumnType("INTEGER")
            .IsRequired(true);
        
        builder.Property(x => x.Comment)
            .HasColumnName("Comment")
            .HasColumnType("VARCHAR(255)")
            .IsRequired(true);
        
        builder.Property(x => x.RatedAt)
            .HasColumnName("RatedAt")
            .IsRequired(true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}