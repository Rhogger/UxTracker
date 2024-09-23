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
    [Migration("20240921230838_v1.9-auth")]
    partial class v19auth
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

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Researcher", b =>
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

                    b.ToTable("Researchers", (string)null);
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Reviewer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("BIT")
                        .HasColumnName("IsActivate");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviewers", (string)null);
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
                            Id = new Guid("1464fec5-767d-42df-b7b9-ce7ca72ba876"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("3dd60540-56fd-4ba4-a1f7-c48eb2da9b09"),
                            Name = "Researcher"
                        },
                        new
                        {
                            Id = new Guid("a1f008c5-64a9-4ee3-a782-7fbdee351784"),
                            Name = "Reviewer"
                        });
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Research.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConsentTermHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(64)")
                        .HasColumnName("ConsentTermHash");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate");

                    b.Property<int>("LastSurveyCollection")
                        .HasColumnType("INTEGER")
                        .HasColumnName("LastSurveyCollection");

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
                            Id = new Guid("6dfaa091-b10f-49ee-92b1-e1823f1a7bf8"),
                            Title = "Visão geral da evolução das avaliações"
                        },
                        new
                        {
                            Id = new Guid("01666c38-a647-4f1c-b1c2-0c813c613840"),
                            Title = "Avaliações de cada usuário por período"
                        },
                        new
                        {
                            Id = new Guid("2c4c2af0-d720-4058-9fef-953d17c92fd0"),
                            Title = "Distribuição das avaliações por período"
                        },
                        new
                        {
                            Id = new Guid("5547634f-6d02-4ca7-9a42-1dd10a5f0c07"),
                            Title = "Frequência das avaliações por período de tempo"
                        },
                        new
                        {
                            Id = new Guid("997af5c3-9a33-48d2-b652-6a230e967d47"),
                            Title = "Número adequado de clusters de usuário"
                        },
                        new
                        {
                            Id = new Guid("c3b150ad-8201-403a-a50a-55c1d83ae455"),
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

                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.Researcher", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.Reviewer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Researcher", b =>
                {
                    b.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ResearcherId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("Email");

                            b1.HasKey("ResearcherId");

                            b1.ToTable("Researchers");

                            b1.WithOwner()
                                .HasForeignKey("ResearcherId");

                            b1.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Verification", "Verification", b2 =>
                                {
                                    b2.Property<Guid>("EmailResearcherId")
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

                                    b2.HasKey("EmailResearcherId");

                                    b2.ToTable("Researchers");

                                    b2.WithOwner()
                                        .HasForeignKey("EmailResearcherId");
                                });

                            b1.Navigation("Verification")
                                .IsRequired();
                        });

                    b.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("ResearcherId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(75)
                                .HasColumnType("NVARCHAR")
                                .HasColumnName("PasswordHash");

                            b1.HasKey("ResearcherId");

                            b1.ToTable("Researchers");

                            b1.WithOwner()
                                .HasForeignKey("ResearcherId");

                            b1.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Verification", "ResetCode", b2 =>
                                {
                                    b2.Property<Guid>("PasswordResearcherId")
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

                                    b2.HasKey("PasswordResearcherId");

                                    b2.ToTable("Researchers");

                                    b2.WithOwner()
                                        .HasForeignKey("PasswordResearcherId");
                                });

                            b1.Navigation("ResetCode");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Reviewer", b =>
                {
                    b.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ReviewerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("Email");

                            b1.HasKey("ReviewerId");

                            b1.ToTable("Reviewers");

                            b1.WithOwner()
                                .HasForeignKey("ReviewerId");

                            b1.OwnsOne("UxTracker.Core.Contexts.Account.ValueObjects.Verification", "Verification", b2 =>
                                {
                                    b2.Property<Guid>("EmailReviewerId")
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

                                    b2.HasKey("EmailReviewerId");

                                    b2.ToTable("Reviewers");

                                    b2.WithOwner()
                                        .HasForeignKey("EmailReviewerId");
                                });

                            b1.Navigation("Verification")
                                .IsRequired();
                        });

                    b.Navigation("Email")
                        .IsRequired();
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Research.Entities.Project", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.Researcher", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Researcher", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
