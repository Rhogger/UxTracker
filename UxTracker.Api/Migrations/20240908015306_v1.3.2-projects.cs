using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v132projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("094b05cc-81d2-41a6-934e-61c5a5bec5b8"), "Frequência das avaliações por período de tempo" },
                    { new Guid("2d9dc01d-9e86-402a-b016-36c869476102"), "Número adequado de clusters de usuário" },
                    { new Guid("39d5a494-b982-43d0-b8ac-3ca0b20deced"), "Avaliações de cada usuário por período" },
                    { new Guid("5dbaba1b-acb9-4247-818b-de45d5bf1853"), "Visão geral da evolução das avaliações" },
                    { new Guid("b08a5fbf-10b3-4ca0-a9b7-5c9a5f258f54"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("f69971cb-c352-4711-824b-5bc3da58f791"), "Distribuição das avaliações por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("28e0c3e6-af82-48c6-af3c-d72fe34e0ecc"), "Reviewer" },
                    { new Guid("621097ea-24ec-4c50-a546-4b5869a89fd2"), "Researcher" },
                    { new Guid("c2f294b2-46af-4d4c-8779-a8dc2ec64545"), "Admin" }
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
                keyValue: new Guid("094b05cc-81d2-41a6-934e-61c5a5bec5b8"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("2d9dc01d-9e86-402a-b016-36c869476102"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("39d5a494-b982-43d0-b8ac-3ca0b20deced"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("5dbaba1b-acb9-4247-818b-de45d5bf1853"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("b08a5fbf-10b3-4ca0-a9b7-5c9a5f258f54"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("f69971cb-c352-4711-824b-5bc3da58f791"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28e0c3e6-af82-48c6-af3c-d72fe34e0ecc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("621097ea-24ec-4c50-a546-4b5869a89fd2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c2f294b2-46af-4d4c-8779-a8dc2ec64545"));

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
    }
}
