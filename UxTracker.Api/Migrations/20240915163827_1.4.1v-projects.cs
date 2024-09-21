using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class _141vprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ConsentTermHash",
                table: "Projects",
                type: "NVARCHAR",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LastSurveyCollection",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("1dc5e1a1-a4b3-49b0-8e8c-1ec755ff1a1a"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("3a5f8b1d-764f-42e6-a9d3-19d6d8aac766"), "Frequência das avaliações por período de tempo" },
                    { new Guid("43299ba8-0568-4464-a383-56b1d7f7dbbd"), "Visão geral da evolução das avaliações" },
                    { new Guid("917b6af9-f3a4-4f96-9013-f98ce6e6a552"), "Número adequado de clusters de usuário" },
                    { new Guid("cdf9d78a-84ac-48f3-8589-a92a1a1a07df"), "Avaliações de cada usuário por período" },
                    { new Guid("d92f9d83-945f-45a8-a70b-283c631da551"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1a8f56dc-605d-42c6-a5bd-c9c0d427c3a9"), "Admin" },
                    { new Guid("1fdec96d-b6b5-4ad0-834d-d236b93312d4"), "Researcher" },
                    { new Guid("7d84d36d-21a1-4ffa-bc7b-990a5a2ac98d"), "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("1dc5e1a1-a4b3-49b0-8e8c-1ec755ff1a1a"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("3a5f8b1d-764f-42e6-a9d3-19d6d8aac766"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("43299ba8-0568-4464-a383-56b1d7f7dbbd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("917b6af9-f3a4-4f96-9013-f98ce6e6a552"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("cdf9d78a-84ac-48f3-8589-a92a1a1a07df"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d92f9d83-945f-45a8-a70b-283c631da551"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1a8f56dc-605d-42c6-a5bd-c9c0d427c3a9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1fdec96d-b6b5-4ad0-834d-d236b93312d4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d84d36d-21a1-4ffa-bc7b-990a5a2ac98d"));

            migrationBuilder.DropColumn(
                name: "ConsentTermHash",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LastSurveyCollection",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "IdConsentTerm",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
