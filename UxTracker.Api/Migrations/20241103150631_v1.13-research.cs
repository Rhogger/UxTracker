using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v113research : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ClusterNumber",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("734a51a9-f130-45c2-bd6a-07ca4e72bfcf"), "Número adequado de clusters de usuário" },
                    { new Guid("7434c19a-501b-455c-8741-d3ae6480df68"), "Frequência das avaliações por período de tempo" },
                    { new Guid("7dd83ed9-747e-4fe2-853e-819eca046aec"), "Visão geral da evolução das avaliações" },
                    { new Guid("86899042-3360-483e-bfc2-cee243c7b4a8"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("8d018137-ba6d-417c-b1c2-63e103e6858d"), "Avaliações de cada usuário por período" },
                    { new Guid("90824ab9-b4b0-48aa-a080-215fbcd6fc45"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("36681477-a691-442c-84f3-6a7daaca5bef"), "Reviewer" },
                    { new Guid("a8a54c26-7e79-4674-98b1-50e7e3b7d6a2"), "Admin" },
                    { new Guid("edc2d66e-c78b-4193-937d-2bd52841baa0"), "Researcher" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("734a51a9-f130-45c2-bd6a-07ca4e72bfcf"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("7434c19a-501b-455c-8741-d3ae6480df68"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("7dd83ed9-747e-4fe2-853e-819eca046aec"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("86899042-3360-483e-bfc2-cee243c7b4a8"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("8d018137-ba6d-417c-b1c2-63e103e6858d"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("90824ab9-b4b0-48aa-a080-215fbcd6fc45"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("36681477-a691-442c-84f3-6a7daaca5bef"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a8a54c26-7e79-4674-98b1-50e7e3b7d6a2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("edc2d66e-c78b-4193-937d-2bd52841baa0"));

            migrationBuilder.DropColumn(
                name: "ClusterNumber",
                table: "Projects");

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
    }
}
