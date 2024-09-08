using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v12projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("252696d3-5124-4988-ab5e-df8056e715e4"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("257977ca-965e-40b8-a9be-e15237f67dce"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("54bf1b93-86dc-4086-beae-52c963323c76"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("589007a3-735b-4dd2-abb3-1afc45cc6de7"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6150d46c-13a9-41dc-9634-a54e5a809d66"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d7acb783-1b31-4536-9b79-f32d8ac0b2a0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("01267e3c-0b5a-46e8-88c2-18cb7b392b94"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1dcfd77d-9e5e-4e31-960b-7562e56930ca"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a427bc0e-d3d9-4549-92ea-37df3cf43a93"));

            migrationBuilder.AddColumn<byte[]>(
                name: "ConsentTerm",
                table: "Projects",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0ec06a18-3b0c-4090-a835-6d71d7a8a28f"), "Número adequado de clusters de usuário" },
                    { new Guid("14414e3b-a123-4559-ab4f-f9a7dc831178"), "Avaliações de cada usuário por período" },
                    { new Guid("18d655d7-7486-4efa-aa4e-1113373be394"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("9f179b4b-c641-41a5-8ac2-bf20ac80401c"), "Frequência das avaliações por período de tempo" },
                    { new Guid("a2a8934c-f696-4334-b9e8-5f671c658946"), "Visão geral da evolução das avaliações" },
                    { new Guid("de66a40d-8b89-420d-91c3-47626cfa2822"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0ddb8830-e63f-4631-8edd-14ea1a398981"), "Admin" },
                    { new Guid("5e4eb8db-6715-47e4-8ea6-7d367a0e7c9b"), "Researcher" },
                    { new Guid("a5236637-eb8f-4906-a1d0-baf49098df8d"), "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("0ec06a18-3b0c-4090-a835-6d71d7a8a28f"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("14414e3b-a123-4559-ab4f-f9a7dc831178"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("18d655d7-7486-4efa-aa4e-1113373be394"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9f179b4b-c641-41a5-8ac2-bf20ac80401c"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("a2a8934c-f696-4334-b9e8-5f671c658946"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("de66a40d-8b89-420d-91c3-47626cfa2822"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0ddb8830-e63f-4631-8edd-14ea1a398981"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5e4eb8db-6715-47e4-8ea6-7d367a0e7c9b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a5236637-eb8f-4906-a1d0-baf49098df8d"));

            migrationBuilder.DropColumn(
                name: "ConsentTerm",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("252696d3-5124-4988-ab5e-df8056e715e4"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("257977ca-965e-40b8-a9be-e15237f67dce"), "Visão geral da evolução das avaliações" },
                    { new Guid("54bf1b93-86dc-4086-beae-52c963323c76"), "Avaliações de cada usuário por período" },
                    { new Guid("589007a3-735b-4dd2-abb3-1afc45cc6de7"), "Distribuição das avaliações por período" },
                    { new Guid("6150d46c-13a9-41dc-9634-a54e5a809d66"), "Número adequado de clusters de usuário" },
                    { new Guid("d7acb783-1b31-4536-9b79-f32d8ac0b2a0"), "Frequência das avaliações por período de tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01267e3c-0b5a-46e8-88c2-18cb7b392b94"), "Reviewer" },
                    { new Guid("1dcfd77d-9e5e-4e31-960b-7562e56930ca"), "Researcher" },
                    { new Guid("a427bc0e-d3d9-4549-92ea-37df3cf43a93"), "Admin" }
                });
        }
    }
}
