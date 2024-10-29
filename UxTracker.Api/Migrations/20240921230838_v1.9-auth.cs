using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v19auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("03b4030b-98e7-4369-9944-161e9b7b2b42"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("2fb15f28-9d60-4445-b922-cd89d65b863e"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6824c990-6ad4-4b01-b13e-a70087cc1efd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("744d8952-c543-44cc-a0a5-4e67fd5aa470"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("7b3bf782-d001-46cb-8a89-ec8115bc6306"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("80d22682-2eef-4ec2-8d88-172d6b5e9594"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("15b79921-9da1-4eb4-9337-725b3c658fd4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2ae5d9a0-8f30-4975-9e69-1afac3a83a90"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("68ddb9a3-3c23-4035-9e65-9645b42e9dfb"));

            migrationBuilder.CreateTable(
                name: "Researchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(75)", maxLength: 75, nullable: false),
                    PasswordResetCode = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: true),
                    PasswordResetExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "NVARCHAR(6)", maxLength: 6, nullable: false),
                    EmailVerificationExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActivate = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviewers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "NVARCHAR(6)", maxLength: 6, nullable: false),
                    EmailVerificationExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActivate = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviewers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("01666c38-a647-4f1c-b1c2-0c813c613840"), "Avaliações de cada usuário por período" },
                    { new Guid("2c4c2af0-d720-4058-9fef-953d17c92fd0"), "Distribuição das avaliações por período" },
                    { new Guid("5547634f-6d02-4ca7-9a42-1dd10a5f0c07"), "Frequência das avaliações por período de tempo" },
                    { new Guid("6dfaa091-b10f-49ee-92b1-e1823f1a7bf8"), "Visão geral da evolução das avaliações" },
                    { new Guid("997af5c3-9a33-48d2-b652-6a230e967d47"), "Número adequado de clusters de usuário" },
                    { new Guid("c3b150ad-8201-403a-a50a-55c1d83ae455"), "Média da experiência do usuário ao longo do tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1464fec5-767d-42df-b7b9-ce7ca72ba876"), "Admin" },
                    { new Guid("3dd60540-56fd-4ba4-a1f7-c48eb2da9b09"), "Researcher" },
                    { new Guid("a1f008c5-64a9-4ee3-a782-7fbdee351784"), "Reviewer" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Researchers_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Researchers_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Reviewers_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Researchers_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Researchers_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Reviewers_UserId",
                table: "UserRole");

            migrationBuilder.DropTable(
                name: "Researchers");

            migrationBuilder.DropTable(
                name: "Reviewers");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("01666c38-a647-4f1c-b1c2-0c813c613840"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("2c4c2af0-d720-4058-9fef-953d17c92fd0"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("5547634f-6d02-4ca7-9a42-1dd10a5f0c07"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6dfaa091-b10f-49ee-92b1-e1823f1a7bf8"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("997af5c3-9a33-48d2-b652-6a230e967d47"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("c3b150ad-8201-403a-a50a-55c1d83ae455"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1464fec5-767d-42df-b7b9-ce7ca72ba876"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dd60540-56fd-4ba4-a1f7-c48eb2da9b09"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a1f008c5-64a9-4ee3-a782-7fbdee351784"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActivate = table.Column<bool>(type: "BIT", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "NVARCHAR(6)", maxLength: 6, nullable: false),
                    EmailVerificationExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(75)", maxLength: 75, nullable: false),
                    PasswordResetCode = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: true),
                    PasswordResetExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("03b4030b-98e7-4369-9944-161e9b7b2b42"), "Frequência das avaliações por período de tempo" },
                    { new Guid("2fb15f28-9d60-4445-b922-cd89d65b863e"), "Avaliações de cada usuário por período" },
                    { new Guid("6824c990-6ad4-4b01-b13e-a70087cc1efd"), "Distribuição das avaliações por período" },
                    { new Guid("744d8952-c543-44cc-a0a5-4e67fd5aa470"), "Visão geral da evolução das avaliações" },
                    { new Guid("7b3bf782-d001-46cb-8a89-ec8115bc6306"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("80d22682-2eef-4ec2-8d88-172d6b5e9594"), "Número adequado de clusters de usuário" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15b79921-9da1-4eb4-9337-725b3c658fd4"), "Admin" },
                    { new Guid("2ae5d9a0-8f30-4975-9e69-1afac3a83a90"), "Reviewer" },
                    { new Guid("68ddb9a3-3c23-4035-9e65-9645b42e9dfb"), "Researcher" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
