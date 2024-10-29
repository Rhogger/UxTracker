using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v11projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("0352e0a1-cac6-47b4-a725-4d407a255045"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("50b908b8-f909-4684-bcfd-cbba4a3adf81"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9dee52da-661d-42b7-9cde-bcd74cf48dc7"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ab9e7e3e-ddbc-4f3a-8b61-4c52397bb833"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ac33ef10-c5c7-4138-aa23-e71696d8951d"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d3b582d1-50ad-433f-8824-9822297889cd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("81c63388-1920-4645-8d87-05050008d7a3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b4d9fdde-5e66-468c-91c4-0231a6ec15d3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d143d4ae-c739-406d-8207-e1f32734cdc0"));

            migrationBuilder.DropColumn(
                name: "ConsentTerm",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("1e744ea5-beb9-41e9-81f0-65886eadb615"), "Número adequado de clusters de usuário" },
                    { new Guid("53aafaa8-96bf-4048-9bf9-ab07a07c75d9"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("5cd3099e-40d6-4822-a7c3-8a4e028587f9"), "Distribuição das avaliações por período" },
                    { new Guid("85ecd690-ce57-45bc-b814-ec7ae6c94060"), "Frequência das avaliações por período de tempo" },
                    { new Guid("9c05c58b-6740-464b-b9fa-8b81ed0e4e92"), "Avaliações de cada usuário por período" },
                    { new Guid("ddd70248-ce2a-44f6-9f49-db457935bbb0"), "Visão geral da evolução das avaliações" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("cc60c390-f650-4ffe-a47b-6e67f064c203"), "Admin" },
                    { new Guid("d7e62935-1a11-4e8a-90c3-937b8b16bf80"), "Researcher" },
                    { new Guid("f9f0cbed-bbfa-4e38-8018-82432d55526d"), "Reviewer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("1e744ea5-beb9-41e9-81f0-65886eadb615"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("53aafaa8-96bf-4048-9bf9-ab07a07c75d9"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("5cd3099e-40d6-4822-a7c3-8a4e028587f9"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("85ecd690-ce57-45bc-b814-ec7ae6c94060"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("9c05c58b-6740-464b-b9fa-8b81ed0e4e92"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ddd70248-ce2a-44f6-9f49-db457935bbb0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc60c390-f650-4ffe-a47b-6e67f064c203"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d7e62935-1a11-4e8a-90c3-937b8b16bf80"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f9f0cbed-bbfa-4e38-8018-82432d55526d"));

            migrationBuilder.AddColumn<string>(
                name: "ConsentTerm",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0352e0a1-cac6-47b4-a725-4d407a255045"), "Visão geral da evolução das avaliações" },
                    { new Guid("50b908b8-f909-4684-bcfd-cbba4a3adf81"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("9dee52da-661d-42b7-9cde-bcd74cf48dc7"), "Número adequado de clusters de usuário" },
                    { new Guid("ab9e7e3e-ddbc-4f3a-8b61-4c52397bb833"), "Avaliações de cada usuário por período" },
                    { new Guid("ac33ef10-c5c7-4138-aa23-e71696d8951d"), "Distribuição das avaliações por período" },
                    { new Guid("d3b582d1-50ad-433f-8824-9822297889cd"), "Frequência das avaliações por período de tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("81c63388-1920-4645-8d87-05050008d7a3"), "Reviewer" },
                    { new Guid("b4d9fdde-5e66-468c-91c4-0231a6ec15d3"), "Researcher" },
                    { new Guid("d143d4ae-c739-406d-8207-e1f32734cdc0"), "Admin" }
                });
        }
    }
}
