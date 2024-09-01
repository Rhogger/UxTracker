using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Infra.Contexts.Account.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired(true);

        builder
            .OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired(true);

        builder
            .OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.Code)
            .HasColumnName("EmailVerificationCode")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(6)
            .IsRequired(true);

        builder
            .OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.ExpireAt)
            .HasColumnName("EmailVerificationExpireAt")
            .IsRequired(false);

        builder
            .OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .IsRequired(false);

        builder
            .OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Ignore(x => x.IsActive);

        builder
            .OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(75)
            .IsRequired(true);

        builder
            .OwnsOne(x => x.Password)
            .OwnsOne(x => x.ResetCode)
            .Property(x => x.Code)
            .HasColumnName("PasswordResetCode")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(8)
            .IsRequired(false);
        
        builder
            .OwnsOne(x => x.Password)
            .OwnsOne(x => x.ResetCode)
            .Property(x => x.ExpireAt)
            .HasColumnName("PasswordResetExpireAt")
            .IsRequired(false);
        
        builder
            .OwnsOne(x => x.Password)
            .OwnsOne(x => x.ResetCode)
            .Property(x => x.VerifiedAt)
            .HasColumnName("PasswordResetVerifiedAt")
            .IsRequired(false);
        
        builder.OwnsOne(x => x.Password)
            .OwnsOne(x => x.ResetCode)
            .Ignore(x => x.IsActive);

        builder
            .Property(x => x.IsActive)
            .HasColumnName("IsActivate")
            .HasColumnType("BIT")
            .IsRequired(true);

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade));

    }
}