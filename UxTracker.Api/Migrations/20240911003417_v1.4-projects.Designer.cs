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
    [Migration("20240911003417_v1.4-projects")]
    partial class v14projects
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
                            Id = new Guid("f57a9e73-193a-4f3c-bdf2-9e6ebc148695"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("56e2edb7-1cd5-4fe8-b205-131d8f23e912"),
                            Name = "Researcher"
                        },
                        new
                        {
                            Id = new Guid("f4ce3b4b-b4a5-49e1-a9ae-6f8cfa5c13c9"),
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

                    b.Property<Guid>("ConsentTermId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdConsentTerm");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<string>("PeriodType")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(10)")
                        .HasColumnName("PeriodType");

                    b.Property<int>("ReviewersCount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ReviewersCount");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Status");

                    b.Property<int>("SurveyCollections")
                        .HasColumnType("INTEGER")
                        .HasColumnName("SurveyCollections");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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
                            Id = new Guid("8783792d-b212-4725-a701-2422c130d58b"),
                            Title = "Visão geral da evolução das avaliações"
                        },
                        new
                        {
                            Id = new Guid("6fd2f13d-4a93-4422-af82-e68acaa93c3a"),
                            Title = "Avaliações de cada usuário por período"
                        },
                        new
                        {
                            Id = new Guid("f1979883-6473-4034-a967-637ec248390d"),
                            Title = "Distribuição das avaliações por período"
                        },
                        new
                        {
                            Id = new Guid("b5a11223-572c-4ecf-b0ec-eabad7bb9d17"),
                            Title = "Frequência das avaliações por período de tempo"
                        },
                        new
                        {
                            Id = new Guid("46640be5-add0-4c84-a3c0-a4d264373127"),
                            Title = "Número adequado de clusters de usuário"
                        },
                        new
                        {
                            Id = new Guid("393d631b-799e-4b59-8238-0c4a5d678b51"),
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

            modelBuilder.Entity("UxTracker.Core.Contexts.Research.Entities.Project", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.User", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
