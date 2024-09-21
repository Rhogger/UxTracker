using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class _142vprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ConsentTermHash",
                table: "Projects",
                type: "NVARCHAR(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "ConsentTermHash",
                table: "Projects",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)");

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
    }
}
