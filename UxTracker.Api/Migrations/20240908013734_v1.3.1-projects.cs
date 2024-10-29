using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v131projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0c2366f5-a381-4867-9657-d4d99ef33686"), "Visão geral da evolução das avaliações" },
                    { new Guid("4524e839-294a-4334-817e-5627817149c3"), "Distribuição das avaliações por período" },
                    { new Guid("71d15b14-d950-4df0-8186-06122ed19826"), "Avaliações de cada usuário por período" },
                    { new Guid("d0ffc234-b086-492c-801a-5d1821efa58f"), "Frequência das avaliações por período de tempo" },
                    { new Guid("eb738ed4-abe9-48d8-8c7d-0e2858ad3ebd"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("f984c20c-c697-44c0-b895-1609d513755a"), "Número adequado de clusters de usuário" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("181ef816-8e49-43a8-bd19-786d584f740f"), "Admin" },
                    { new Guid("3909cd58-6368-4b97-b0d8-b5c164f81eea"), "Reviewer" },
                    { new Guid("bd7b541e-28ff-486e-a5d1-ce91e8ae9be3"), "Researcher" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_Id",
                table: "Projects",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_Id",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("0c2366f5-a381-4867-9657-d4d99ef33686"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4524e839-294a-4334-817e-5627817149c3"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("71d15b14-d950-4df0-8186-06122ed19826"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d0ffc234-b086-492c-801a-5d1821efa58f"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("eb738ed4-abe9-48d8-8c7d-0e2858ad3ebd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("f984c20c-c697-44c0-b895-1609d513755a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("181ef816-8e49-43a8-bd19-786d584f740f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3909cd58-6368-4b97-b0d8-b5c164f81eea"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd7b541e-28ff-486e-a5d1-ce91e8ae9be3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
