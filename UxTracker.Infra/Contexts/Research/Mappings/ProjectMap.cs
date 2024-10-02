using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Infra.Contexts.Research.Mappings;

public class ProjectMap: IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired(true);
        
        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired(true);
        
        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(15)
            .IsRequired(true);
        
        builder.Property(x => x.StartDate)
            .HasColumnName("StartDate")
            .IsRequired(true);
        
        builder.Property(x => x.EndDate)
            .HasColumnName("EndDate")
            .IsRequired(false);
        
        builder.Property(x => x.PeriodType)
            .HasColumnName("PeriodType")
            .HasColumnType("NVARCHAR(10)")
            .IsRequired(true);
        
        builder.Property(x => x.SurveyCollections)
            .HasColumnName("SurveyCollections")
            .HasColumnType("INTEGER")
            .IsRequired(true);
        
        builder.Property(x => x.LastSurveyCollection)
            .HasColumnName("LastSurveyCollection")
            .HasColumnType("INTEGER")
            .IsRequired(true);

        builder.Property(x => x.ConsentTermHash)
            .HasColumnName("ConsentTermHash")
            .HasColumnType("NVARCHAR(64)")
            .IsRequired(true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Relatories)
            .WithMany(x => x.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectRelatory",
                relatory => relatory
                    .HasOne<Relatory>()
                    .WithMany()
                    .HasForeignKey("RelatoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                ,
                project => project
                    .HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("ProjectId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
        
        builder.HasMany(x => x.Reviews)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}