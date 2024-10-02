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
            .OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Address)
                    .HasColumnName("Email")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(255)
                    .IsRequired(true);

                email.OwnsOne(x => x.Verification, verification =>
                {
                    verification.Property(x => x.Code)
                        .HasColumnName("EmailVerificationCode")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(6)
                        .IsRequired(true);
                    
                    verification.Property(x => x.ExpireAt)
                        .HasColumnName("EmailVerificationExpireAt")
                        .IsRequired(false);
                    
                    verification.Property(x => x.VerifiedAt)
                        .HasColumnName("EmailVerificationVerifiedAt")
                        .IsRequired(false);
                    
                    verification.Ignore(x => x.IsActive);
                });
            });

        builder
            .OwnsOne(x => x.Password, password =>
            {
                password.Property(x => x.Hash)
                    .HasColumnName("PasswordHash")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(75)
                    .IsRequired(false);
                
                password.Property<bool>("PasswordExists")
                    .HasColumnName("PasswordExists")
                    .HasColumnType("BIT")
                    .IsRequired()
                    .HasDefaultValue(true);

                password.OwnsOne(x => x.ResetCode, resetCode =>
                {
                    resetCode.Property(x => x.Code)
                        .HasColumnName("PasswordResetCode")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(8)
                        .IsRequired(false);
                    
                    resetCode.Property(x => x.ExpireAt)
                        .HasColumnName("PasswordResetExpireAt")
                        .IsRequired(false);
                    
                    resetCode.Property(x => x.VerifiedAt)
                        .HasColumnName("PasswordResetVerifiedAt")
                        .IsRequired(false);
                    
                    resetCode.Ignore(x => x.IsActive);
                });
                    
            });

        builder
            .Property(x => x.IsActive)
            .HasColumnName("IsActivate")
            .HasColumnType("BIT")
            .IsRequired(true);

        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                ,
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}