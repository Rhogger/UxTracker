﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UxTracker.Infra.Data;

#nullable disable

namespace UxTracker.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240830222219_v1.8.1-auth")]
    partial class v181auth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("73bc5e43-a806-46c9-becf-f778e7031069"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("825533a5-e09d-431c-bd5a-750261e6b94e"),
                            Name = "Researcher"
                        },
                        new
                        {
                            Id = new Guid("62d51832-25cf-457d-b192-1c281de0c64d"),
                            Name = "Reviewer"
                        });
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("BIT")
                        .HasColumnName("IsActivate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.User", b =>
                {
                    b.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Verification", "Verification", b2 =>
                                {
                                    b2.Property<Guid>("EmailUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Code")
                                        .IsRequired()
                                        .HasMaxLength(6)
                                        .HasColumnType("NVARCHAR")
                                        .HasColumnName("EmailVerificationCode");

                                    b2.Property<DateTime?>("ExpireAt")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("EmailVerificationExpireAt");

                                    b2.Property<DateTime?>("VerifiedAt")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("EmailVerificationVerifiedAt");

                                    b2.HasKey("EmailUserId");

                                    b2.ToTable("Users");

                                    b2.WithOwner()
                                        .HasForeignKey("EmailUserId");
                                });

                            b1.Navigation("Verification")
                                .IsRequired();
                        });

                    b.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(75)
                                .HasColumnType("NVARCHAR")
                                .HasColumnName("PasswordHash");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Verification", "ResetCode", b2 =>
                                {
                                    b2.Property<Guid>("PasswordUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Code")
                                        .HasMaxLength(8)
                                        .HasColumnType("NVARCHAR")
                                        .HasColumnName("PasswordResetCode");

                                    b2.Property<DateTime?>("ExpireAt")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("PasswordResetExpireAt");

                                    b2.Property<DateTime?>("VerifiedAt")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("PasswordResetVerifiedAt");

                                    b2.HasKey("PasswordUserId");

                                    b2.ToTable("Users");

                                    b2.WithOwner()
                                        .HasForeignKey("PasswordUserId");
                                });

                            b1.Navigation("ResetCode");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
