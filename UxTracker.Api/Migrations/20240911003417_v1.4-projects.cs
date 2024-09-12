using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v14projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("094b05cc-81d2-41a6-934e-61c5a5bec5b8"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("2d9dc01d-9e86-402a-b016-36c869476102"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("39d5a494-b982-43d0-b8ac-3ca0b20deced"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("5dbaba1b-acb9-4247-818b-de45d5bf1853"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("b08a5fbf-10b3-4ca0-a9b7-5c9a5f258f54"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("f69971cb-c352-4711-824b-5bc3da58f791"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28e0c3e6-af82-48c6-af3c-d72fe34e0ecc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("621097ea-24ec-4c50-a546-4b5869a89fd2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c2f294b2-46af-4d4c-8779-a8dc2ec64545"));

            migrationBuilder.DropColumn(
                name: "ConsentTerm",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 80);

            migrationBuilder.AddColumn<Guid>(
                name: "IdConsentTerm",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SurveyCollections",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("393d631b-799e-4b59-8238-0c4a5d678b51"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("46640be5-add0-4c84-a3c0-a4d264373127"), "Número adequado de clusters de usuário" },
                    { new Guid("6fd2f13d-4a93-4422-af82-e68acaa93c3a"), "Avaliações de cada usuário por período" },
                    { new Guid("8783792d-b212-4725-a701-2422c130d58b"), "Visão geral da evolução das avaliações" },
                    { new Guid("b5a11223-572c-4ecf-b0ec-eabad7bb9d17"), "Frequência das avaliações por período de tempo" },
                    { new Guid("f1979883-6473-4034-a967-637ec248390d"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("56e2edb7-1cd5-4fe8-b205-131d8f23e912"), "Researcher" },
                    { new Guid("f4ce3b4b-b4a5-49e1-a9ae-6f8cfa5c13c9"), "Reviewer" },
                    { new Guid("f57a9e73-193a-4f3c-bdf2-9e6ebc148695"), "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("393d631b-799e-4b59-8238-0c4a5d678b51"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("46640be5-add0-4c84-a3c0-a4d264373127"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6fd2f13d-4a93-4422-af82-e68acaa93c3a"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("8783792d-b212-4725-a701-2422c130d58b"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("b5a11223-572c-4ecf-b0ec-eabad7bb9d17"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("f1979883-6473-4034-a967-637ec248390d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("56e2edb7-1cd5-4fe8-b205-131d8f23e912"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4ce3b4b-b4a5-49e1-a9ae-6f8cfa5c13c9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f57a9e73-193a-4f3c-bdf2-9e6ebc148695"));

            migrationBuilder.DropColumn(
                name: "IdConsentTerm",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SurveyCollections",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                maxLength: 80,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConsentTerm",
                table: "Projects",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte>(
                name: "Period",
                table: "Projects",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("094b05cc-81d2-41a6-934e-61c5a5bec5b8"), "Frequência das avaliações por período de tempo" },
                    { new Guid("2d9dc01d-9e86-402a-b016-36c869476102"), "Número adequado de clusters de usuário" },
                    { new Guid("39d5a494-b982-43d0-b8ac-3ca0b20deced"), "Avaliações de cada usuário por período" },
                    { new Guid("5dbaba1b-acb9-4247-818b-de45d5bf1853"), "Visão geral da evolução das avaliações" },
                    { new Guid("b08a5fbf-10b3-4ca0-a9b7-5c9a5f258f54"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("f69971cb-c352-4711-824b-5bc3da58f791"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("28e0c3e6-af82-48c6-af3c-d72fe34e0ecc"), "Reviewer" },
                    { new Guid("621097ea-24ec-4c50-a546-4b5869a89fd2"), "Researcher" },
                    { new Guid("c2f294b2-46af-4d4c-8779-a8dc2ec64545"), "Admin" }
                });
        }
    }
}
