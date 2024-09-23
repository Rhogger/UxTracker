using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v192auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Researchers_Id",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Reviewers_Id",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4ad8525f-3f58-433b-9fb3-8c814fc815b5"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("83b45c3c-0610-453a-8f16-5b9abad7d4ff"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("91ff8f56-4c20-48da-92b4-dc9a4156cd51"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("de79a69f-7208-4d35-ae6c-85c0f33d27e2"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ded56866-96fc-4565-a5bc-c869133e428c"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ebf42bbd-60d9-4249-9902-2703490dc23d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("01323dfa-a085-4ee2-807c-504de0e1638e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5de19297-30d3-4bb6-81c4-5a3c041855b8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("95814c9d-fe0e-46e4-957c-5c0a6b037c89"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserRole",
                newName: "ResearcherId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewerId",
                table: "UserRole",
                type: "uniqueidentifier",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ResearcherId",
                table: "UserRole",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("4ad8525f-3f58-433b-9fb3-8c814fc815b5"), "Frequência das avaliações por período de tempo" },
                    { new Guid("83b45c3c-0610-453a-8f16-5b9abad7d4ff"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("91ff8f56-4c20-48da-92b4-dc9a4156cd51"), "Número adequado de clusters de usuário" },
                    { new Guid("de79a69f-7208-4d35-ae6c-85c0f33d27e2"), "Visão geral da evolução das avaliações" },
                    { new Guid("ded56866-96fc-4565-a5bc-c869133e428c"), "Distribuição das avaliações por período" },
                    { new Guid("ebf42bbd-60d9-4249-9902-2703490dc23d"), "Avaliações de cada usuário por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01323dfa-a085-4ee2-807c-504de0e1638e"), "Reviewer" },
                    { new Guid("5de19297-30d3-4bb6-81c4-5a3c041855b8"), "Admin" },
                    { new Guid("95814c9d-fe0e-46e4-957c-5c0a6b037c89"), "Researcher" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Researchers_Id",
                table: "UserRole",
                column: "Id",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Reviewers_Id",
                table: "UserRole",
                column: "Id",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
