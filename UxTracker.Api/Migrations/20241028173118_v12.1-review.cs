using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v121review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4204a368-ce70-4508-aef5-1f2c37eeaafd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("467ac362-d75f-48f2-a991-0ebd5d0e922d"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("7e89fd1a-c59c-4e2d-a55f-54b4fe6c45bd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("a3318035-f56b-4977-8a49-75fbf3496a76"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d977b95e-f69b-4ec7-babe-2b6dc11f6dad"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("fecc0080-4f00-4be0-939d-023c8723e512"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("38df6698-4df7-48fb-be3e-05b6fcfe6d1e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("501a2f99-f8b6-47c2-af3b-a5ffd2469123"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("816acd9e-3d8a-4671-b730-521ea0b6d412"));

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("65e80003-298c-401d-b18c-7652c942ce49"), "Frequência das avaliações por período de tempo" },
                    { new Guid("81d07f64-84d7-41a1-b836-2c7f2c36ce72"), "Visão geral da evolução das avaliações" },
                    { new Guid("a4427852-71b7-4bd8-acee-b3436261e814"), "Distribuição das avaliações por período" },
                    { new Guid("bcb23374-e4ad-414c-a28a-9ede7326930c"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("c5f0026d-062b-433a-973f-be7f74604301"), "Número adequado de clusters de usuário" },
                    { new Guid("e02ba04d-e550-4e59-a3a0-11dda84794a4"), "Avaliações de cada usuário por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4be05ca3-7782-481d-b3f0-c748228bf802"), "Researcher" },
                    { new Guid("64401e1d-cac4-4692-8ec9-6cf695d4364a"), "Admin" },
                    { new Guid("76802828-5553-4a6c-b35e-fbd1e51d4205"), "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("65e80003-298c-401d-b18c-7652c942ce49"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("81d07f64-84d7-41a1-b836-2c7f2c36ce72"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("a4427852-71b7-4bd8-acee-b3436261e814"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("bcb23374-e4ad-414c-a28a-9ede7326930c"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("c5f0026d-062b-433a-973f-be7f74604301"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("e02ba04d-e550-4e59-a3a0-11dda84794a4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4be05ca3-7782-481d-b3f0-c748228bf802"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64401e1d-cac4-4692-8ec9-6cf695d4364a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("76802828-5553-4a6c-b35e-fbd1e51d4205"));

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("4204a368-ce70-4508-aef5-1f2c37eeaafd"), "Número adequado de clusters de usuário" },
                    { new Guid("467ac362-d75f-48f2-a991-0ebd5d0e922d"), "Frequência das avaliações por período de tempo" },
                    { new Guid("7e89fd1a-c59c-4e2d-a55f-54b4fe6c45bd"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("a3318035-f56b-4977-8a49-75fbf3496a76"), "Avaliações de cada usuário por período" },
                    { new Guid("d977b95e-f69b-4ec7-babe-2b6dc11f6dad"), "Visão geral da evolução das avaliações" },
                    { new Guid("fecc0080-4f00-4be0-939d-023c8723e512"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("38df6698-4df7-48fb-be3e-05b6fcfe6d1e"), "Researcher" },
                    { new Guid("501a2f99-f8b6-47c2-af3b-a5ffd2469123"), "Reviewer" },
                    { new Guid("816acd9e-3d8a-4671-b730-521ea0b6d412"), "Admin" }
                });
        }
    }
}
