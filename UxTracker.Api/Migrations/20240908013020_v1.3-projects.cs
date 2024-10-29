using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v13projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("093d1b88-91eb-49a0-a835-acf0447fda0c"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("9cf84ada-4333-455e-a663-104acb422baa"), "Avaliações de cada usuário por período" },
                    { new Guid("9dd902d7-1e94-43e3-a1bd-e8e360f7e8f2"), "Número adequado de clusters de usuário" },
                    { new Guid("a8d208c0-4ef7-4927-873b-2a616ca689e4"), "Frequência das avaliações por período de tempo" },
                    { new Guid("e4aa581c-211d-4fef-8bee-fbfb4f7055ab"), "Visão geral da evolução das avaliações" },
                    { new Guid("e4aeab5b-61a3-4317-a4d5-b968fcff00b4"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1b4035bc-9543-4a8a-b3fc-9a167b6a51ef"), "Reviewer" },
                    { new Guid("63032cfd-d6e0-47dc-95c1-af2bf7b24bbd"), "Researcher" },
                    { new Guid("a7fa56ca-f7c4-4384-8aaa-fe2bf99de226"), "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("093d1b88-91eb-49a0-a835-acf0447fda0c"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9cf84ada-4333-455e-a663-104acb422baa"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9dd902d7-1e94-43e3-a1bd-e8e360f7e8f2"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("a8d208c0-4ef7-4927-873b-2a616ca689e4"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("e4aa581c-211d-4fef-8bee-fbfb4f7055ab"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("e4aeab5b-61a3-4317-a4d5-b968fcff00b4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1b4035bc-9543-4a8a-b3fc-9a167b6a51ef"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("63032cfd-d6e0-47dc-95c1-af2bf7b24bbd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a7fa56ca-f7c4-4384-8aaa-fe2bf99de226"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");

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
    }
}
