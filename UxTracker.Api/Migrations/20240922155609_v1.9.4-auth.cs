using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v194auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Researchers_ResearcherId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Reviewers_ReviewerId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_ReviewerId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("0800e3b4-6cd4-4682-a925-85c2c4fe57d8"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6f313efc-d2a3-4a83-9747-afe0386c949c"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("8bc21acd-ae9f-4c47-aa77-b6c581734bd1"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9a4ca48f-3f3b-49b8-a791-6d5d563bd5d9"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9d4050ba-207c-40c8-be09-4d6c1e068621"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d653b2b6-07ef-41c2-b096-4470d5b4c080"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("39860a80-1e1a-4adb-8614-3365863852b9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aa31c843-b2fc-44bf-a4f7-685e8f81dab3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c17769ee-281c-4d68-a809-4020c00cc864"));

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "EmailVerificationCode",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "EmailVerificationExpireAt",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "EmailVerificationVerifiedAt",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "IsActivate",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "EmailVerificationCode",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "EmailVerificationExpireAt",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "EmailVerificationVerifiedAt",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "IsActivate",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "PasswordResetExpireAt",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "PasswordResetVerifiedAt",
                table: "Researchers");

            migrationBuilder.RenameColumn(
                name: "ResearcherId",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Reviewers",
                type: "NVARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Reviewers",
                type: "NVARCHAR(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Reviewers",
                type: "NVARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Reviewers",
                type: "NVARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "NVARCHAR(6)", maxLength: 6, nullable: false),
                    EmailVerificationExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(75)", maxLength: 75, nullable: true),
                    PasswordResetCode = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: true),
                    PasswordResetExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordExists = table.Column<bool>(type: "BIT", nullable: true, defaultValue: true),
                    IsActivate = table.Column<bool>(type: "BIT", nullable: false)
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
                    { new Guid("65e13066-128f-4bd7-a1f1-9c2d040af116"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("b4615a64-9e0f-400c-9ce6-4a76950c8701"), "Número adequado de clusters de usuário" },
                    { new Guid("e2a82cfd-f0e5-4b26-9d7b-5653cd9a90eb"), "Distribuição das avaliações por período" },
                    { new Guid("e9d87f54-5b35-4896-a114-8369a7939c41"), "Frequência das avaliações por período de tempo" },
                    { new Guid("ed5537f3-5d01-47c5-b868-951b8cd24bba"), "Avaliações de cada usuário por período" },
                    { new Guid("f1d54d50-89d3-45e4-92ce-786941d5f02d"), "Visão geral da evolução das avaliações" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("164230e4-389b-4f86-991c-e8072d041c90"), "Researcher" },
                    { new Guid("463c9a71-8960-4f07-9fd2-ee9cb7b2132b"), "Reviewer" },
                    { new Guid("9af29f95-020a-4263-b0f3-c6ec8640278d"), "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Researchers_Users_Id",
                table: "Researchers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviewers_Users_Id",
                table: "Reviewers",
                column: "Id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researchers_Users_Id",
                table: "Researchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviewers_Users_Id",
                table: "Reviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("65e13066-128f-4bd7-a1f1-9c2d040af116"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("b4615a64-9e0f-400c-9ce6-4a76950c8701"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("e2a82cfd-f0e5-4b26-9d7b-5653cd9a90eb"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("e9d87f54-5b35-4896-a114-8369a7939c41"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ed5537f3-5d01-47c5-b868-951b8cd24bba"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("f1d54d50-89d3-45e4-92ce-786941d5f02d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("164230e4-389b-4f86-991c-e8072d041c90"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("463c9a71-8960-4f07-9fd2-ee9cb7b2132b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9af29f95-020a-4263-b0f3-c6ec8640278d"));

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "ResearcherId");

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewerId",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Reviewers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Reviewers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Reviewers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Reviewers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(80)",
                oldMaxLength: 80);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reviewers",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailVerificationCode",
                table: "Reviewers",
                type: "NVARCHAR(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerificationExpireAt",
                table: "Reviewers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerificationVerifiedAt",
                table: "Reviewers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivate",
                table: "Reviewers",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Researchers",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailVerificationCode",
                table: "Researchers",
                type: "NVARCHAR(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerificationExpireAt",
                table: "Researchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerificationVerifiedAt",
                table: "Researchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivate",
                table: "Researchers",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Researchers",
                type: "NVARCHAR(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Researchers",
                type: "NVARCHAR(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetExpireAt",
                table: "Researchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetVerifiedAt",
                table: "Researchers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "ResearcherId", "RoleId" });

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0800e3b4-6cd4-4682-a925-85c2c4fe57d8"), "Distribuição das avaliações por período" },
                    { new Guid("6f313efc-d2a3-4a83-9747-afe0386c949c"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("8bc21acd-ae9f-4c47-aa77-b6c581734bd1"), "Número adequado de clusters de usuário" },
                    { new Guid("9a4ca48f-3f3b-49b8-a791-6d5d563bd5d9"), "Frequência das avaliações por período de tempo" },
                    { new Guid("9d4050ba-207c-40c8-be09-4d6c1e068621"), "Visão geral da evolução das avaliações" },
                    { new Guid("d653b2b6-07ef-41c2-b096-4470d5b4c080"), "Avaliações de cada usuário por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("39860a80-1e1a-4adb-8614-3365863852b9"), "Admin" },
                    { new Guid("aa31c843-b2fc-44bf-a4f7-685e8f81dab3"), "Researcher" },
                    { new Guid("c17769ee-281c-4d68-a809-4020c00cc864"), "Reviewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_ReviewerId",
                table: "UserRole",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Researchers_ResearcherId",
                table: "UserRole",
                column: "ResearcherId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Reviewers_ReviewerId",
                table: "UserRole",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
