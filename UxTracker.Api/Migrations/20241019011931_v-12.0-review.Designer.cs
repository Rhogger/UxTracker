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
    [Migration("20241019011931_v-12.0-review")]
    partial class v120review
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
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
                            Id = new Guid("816acd9e-3d8a-4671-b730-521ea0b6d412"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("38df6698-4df7-48fb-be3e-05b6fcfe6d1e"),
                            Name = "Researcher"
                        },
                        new
                        {
                            Id = new Guid("501a2f99-f8b6-47c2-af3b-a5ffd2469123"),
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
                            Id = new Guid("d977b95e-f69b-4ec7-babe-2b6dc11f6dad"),
                            Title = "Visão geral da evolução das avaliações"
                        },
                        new
                        {
                            Id = new Guid("a3318035-f56b-4977-8a49-75fbf3496a76"),
                            Title = "Avaliações de cada usuário por período"
                        },
                        new
                        {
                            Id = new Guid("fecc0080-4f00-4be0-939d-023c8723e512"),
                            Title = "Distribuição das avaliações por período"
                        },
                        new
                        {
                            Id = new Guid("467ac362-d75f-48f2-a991-0ebd5d0e922d"),
                            Title = "Frequência das avaliações por período de tempo"
                        },
                        new
                        {
                            Id = new Guid("4204a368-ce70-4508-aef5-1f2c37eeaafd"),
                            Title = "Número adequado de clusters de usuário"
                        },
                        new
                        {
                            Id = new Guid("7e89fd1a-c59c-4e2d-a55f-54b4fe6c45bd"),
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

                    b.Property<decimal>("Rating")
                        .HasColumnType("DECIMAL(3,1)")
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

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
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
