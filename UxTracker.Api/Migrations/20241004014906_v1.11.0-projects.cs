using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1110projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("230d81b7-e80a-4e07-aa24-0a0810e1b8b5"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("28a7a185-f704-4d5d-88e5-da6a643cbbbd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4e5a8bdf-516d-4094-960d-9a7116725b97"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("62c408cb-dc25-4209-b405-6befd5dcaa5d"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d9a791fa-0d20-47bd-9683-88bf52b3bfc2"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("dbd22ca3-a9b4-495c-ab87-b69824d8fcc9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e5bbb54-f4f9-4baf-8108-c341436921f9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ac43f877-bd0d-496d-bb21-2aed8f293ddc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f40d2d9d-7185-4142-8b29-97fd4b76375c"));

            migrationBuilder.DropColumn(
                name: "LastSurveyCollection",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("1572d2d0-8c17-4b73-8a0a-b1cd9bfc79a9"), "Frequência das avaliações por período de tempo" },
                    { new Guid("1898e416-4739-4fec-90e3-04f8f9be1d97"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("39601c7c-61b9-4582-8050-bffb4d088d8a"), "Número adequado de clusters de usuário" },
                    { new Guid("4f51dda7-ac6e-4039-8ed4-4261d4521da3"), "Distribuição das avaliações por período" },
                    { new Guid("92bec92f-c4a6-4d58-8af1-e54b407793fe"), "Visão geral da evolução das avaliações" },
                    { new Guid("f1f2f879-fde6-470b-8f1d-1d0be84e7535"), "Avaliações de cada usuário por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b3884d7-96ba-4a1a-8507-968f7019e9aa"), "Researcher" },
                    { new Guid("966e68c5-7293-4204-afe0-ab400bad1d41"), "Admin" },
                    { new Guid("dbc865ab-5ef9-4d8f-8eca-8207899d0afe"), "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("1572d2d0-8c17-4b73-8a0a-b1cd9bfc79a9"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("1898e416-4739-4fec-90e3-04f8f9be1d97"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("39601c7c-61b9-4582-8050-bffb4d088d8a"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4f51dda7-ac6e-4039-8ed4-4261d4521da3"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("92bec92f-c4a6-4d58-8af1-e54b407793fe"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("f1f2f879-fde6-470b-8f1d-1d0be84e7535"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2b3884d7-96ba-4a1a-8507-968f7019e9aa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("966e68c5-7293-4204-afe0-ab400bad1d41"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dbc865ab-5ef9-4d8f-8eca-8207899d0afe"));

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
                    { new Guid("230d81b7-e80a-4e07-aa24-0a0810e1b8b5"), "Avaliações de cada usuário por período" },
                    { new Guid("28a7a185-f704-4d5d-88e5-da6a643cbbbd"), "Visão geral da evolução das avaliações" },
                    { new Guid("4e5a8bdf-516d-4094-960d-9a7116725b97"), "Distribuição das avaliações por período" },
                    { new Guid("62c408cb-dc25-4209-b405-6befd5dcaa5d"), "Número adequado de clusters de usuário" },
                    { new Guid("d9a791fa-0d20-47bd-9683-88bf52b3bfc2"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("dbd22ca3-a9b4-495c-ab87-b69824d8fcc9"), "Frequência das avaliações por período de tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3e5bbb54-f4f9-4baf-8108-c341436921f9"), "Admin" },
                    { new Guid("ac43f877-bd0d-496d-bb21-2aed8f293ddc"), "Researcher" },
                    { new Guid("f40d2d9d-7185-4142-8b29-97fd4b76375c"), "Reviewer" }
                });
        }
    }
}
