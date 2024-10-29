using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v195auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "AcceptedTerms",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptedTerms", x => new { x.UserId, x.ProjectId });
                });

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0b5badda-0858-4c43-aeb1-aebcaef0ba20"), "Visão geral da evolução das avaliações" },
                    { new Guid("21c4d3c8-4c76-4d23-b2e4-eb8c287a17cf"), "Avaliações de cada usuário por período" },
                    { new Guid("6d7f4bed-f8e3-4c95-94c6-f1d2e9633ac7"), "Frequência das avaliações por período de tempo" },
                    { new Guid("79110951-c75f-4430-93cf-389460a37ade"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("9d6ec478-de7c-4a2d-9f4f-5b5741c7c5b4"), "Número adequado de clusters de usuário" },
                    { new Guid("b4d0f526-16f2-441e-9759-a20f43e758af"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17b62251-8654-46a0-91d0-5e19f64cd975"), "Researcher" },
                    { new Guid("2fddbcdc-55df-47fd-9a18-365561930424"), "Admin" },
                    { new Guid("e3dfd953-5c11-43b2-a8b8-28fa9387d19d"), "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcceptedTerms");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("0b5badda-0858-4c43-aeb1-aebcaef0ba20"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("21c4d3c8-4c76-4d23-b2e4-eb8c287a17cf"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6d7f4bed-f8e3-4c95-94c6-f1d2e9633ac7"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("79110951-c75f-4430-93cf-389460a37ade"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9d6ec478-de7c-4a2d-9f4f-5b5741c7c5b4"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("b4d0f526-16f2-441e-9759-a20f43e758af"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("17b62251-8654-46a0-91d0-5e19f64cd975"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2fddbcdc-55df-47fd-9a18-365561930424"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e3dfd953-5c11-43b2-a8b8-28fa9387d19d"));

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
        }
    }
}
