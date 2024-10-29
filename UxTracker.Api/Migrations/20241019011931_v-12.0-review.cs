using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v120review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Reviews",
                type: "DECIMAL(3,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(3,1)");

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
    }
}
