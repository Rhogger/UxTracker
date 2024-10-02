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
    [Migration("20240926021204_v1.10-review")]
    partial class v110review
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
                            Id = new Guid("3e5bbb54-f4f9-4baf-8108-c341436921f9"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("ac43f877-bd0d-496d-bb21-2aed8f293ddc"),
                            Name = "Researcher"
                        },
                        new
                        {
                            Id = new Guid("f40d2d9d-7185-4142-8b29-97fd4b76375c"),
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

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
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
                            Id = new Guid("28a7a185-f704-4d5d-88e5-da6a643cbbbd"),
                            Title = "Visão geral da evolução das avaliações"
                        },
                        new
                        {
                            Id = new Guid("230d81b7-e80a-4e07-aa24-0a0810e1b8b5"),
                            Title = "Avaliações de cada usuário por período"
                        },
                        new
                        {
                            Id = new Guid("4e5a8bdf-516d-4094-960d-9a7116725b97"),
                            Title = "Distribuição das avaliações por período"
                        },
                        new
                        {
                            Id = new Guid("dbd22ca3-a9b4-495c-ab87-b69824d8fcc9"),
                            Title = "Frequência das avaliações por período de tempo"
                        },
                        new
                        {
                            Id = new Guid("62c408cb-dc25-4209-b405-6befd5dcaa5d"),
                            Title = "Número adequado de clusters de usuário"
                        },
                        new
                        {
                            Id = new Guid("d9a791fa-0d20-47bd-9683-88bf52b3bfc2"),
                            Title = "Média da experiência do usuário ao longo do tempo"
                        });
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Review.Entities.Rate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("Comment");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("RatedAt");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Rating");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Review.Entities.UserAcceptedTcle", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProjectId");

                    b.Property<DateTime>("AcceptedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("AcceptedAt");

                    b.HasKey("UserId", "ProjectId");

                    b.ToTable("AcceptedTerms", (string)null);
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Researcher", b =>
                {
                    b.HasBaseType("UxTracker.Core.Contexts.Account.Entities.User");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.ToTable("Researchers", (string)null);
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Reviewer", b =>
                {
                    b.HasBaseType("UxTracker.Core.Contexts.Account.Entities.User");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("BirthDate");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Country");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Sex");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("State");

                    b.ToTable("Reviewers", (string)null);
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
                                .HasMaxLength(75)
                                .HasColumnType("NVARCHAR")
                                .HasColumnName("PasswordHash");

                            b1.Property<bool>("PasswordExists")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("BIT")
                                .HasDefaultValue(true)
                                .HasColumnName("PasswordExists");

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

                    b.Navigation("Password");
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

            modelBuilder.Entity("UxTracker.Core.Contexts.Review.Entities.Rate", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Research.Entities.Project", "Project")
                        .WithMany("Reviews")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.Reviewer", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Researcher", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("UxTracker.Core.Contexts.Account.Entities.Researcher", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Reviewer", b =>
                {
                    b.HasOne("UxTracker.Core.Contexts.Account.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("UxTracker.Core.Contexts.Account.Entities.Reviewer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Research.Entities.Project", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Researcher", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("UxTracker.Core.Contexts.Account.Entities.Reviewer", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}