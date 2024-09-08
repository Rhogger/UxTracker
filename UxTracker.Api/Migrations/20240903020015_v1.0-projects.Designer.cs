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
    [Migration("20240903020015_v1.0-projects")]
    partial class v10projects
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectRelatory", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "RelatoryId");

                    b.HasIndex("RelatoryId");

                    b.ToTable("ProjectRelatory");
                });

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
                            Id = new Guid("d143d4ae-c739-406d-8207-e1f32734cdc0"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("b4d9fdde-5e66-468c-91c4-0231a6ec15d3"),
                            Name = "Researcher"
                        },
                        new
                        {
                            Id = new Guid("81c63388-1920-4645-8d87-05050008d7a3"),
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

            modelBuilder.Entity("UxTracker.Core.Contexts.Research.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConsentTerm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("EndDate")
                        .HasMaxLength(80)
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<byte>("Period")
                        .HasColumnType("TINYINT")
                        .HasColumnName("Period");

                    b.Property<byte>("PeriodType")
                        .HasColumnType("TINYINT")
                        .HasColumnName("PeriodType");

                    b.Property<DateTime>("StartDate")
                        .HasMaxLength(80)
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Research.Entities.Relatory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Relatories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0352e0a1-cac6-47b4-a725-4d407a255045"),
                            Title = "Visão geral da evolução das avaliações"
                        },
                        new
                        {
                            Id = new Guid("ab9e7e3e-ddbc-4f3a-8b61-4c52397bb833"),
                            Title = "Avaliações de cada usuário por período"
                        },
                        new
                        {
                            Id = new Guid("ac33ef10-c5c7-4138-aa23-e71696d8951d"),
                            Title = "Distribuição das avaliações por período"
                        },
                        new
                        {
                            Id = new Guid("d3b582d1-50ad-433f-8824-9822297889cd"),
                            Title = "Frequência das avaliações por período de tempo"
                        },
                        new
                        {
                            Id = new Guid("9dee52da-661d-42b7-9cde-bcd74cf48dc7"),
                            Title = "Número adequado de clusters de usuário"
                        },
                        new
                        {
                            Id = new Guid("50b908b8-f909-4684-bcfd-cbba4a3adf81"),
                            Title = "Média da experiência do usuário ao longo do tempo"
                        });
                });

            modelBuilder.Entity("ProjectRelatory", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Research.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UxTracker.Core.Contexts.Research.Entities.Relatory", null)
                        .WithMany()
                        .HasForeignKey("RelatoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
