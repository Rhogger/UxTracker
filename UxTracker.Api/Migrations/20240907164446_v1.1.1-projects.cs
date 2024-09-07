using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v111projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PeriodType",
                table: "Projects",
                type: "NVARCHAR(10)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("50b93672-380b-4244-b99a-0ec5575c9d3b"), "Visão geral da evolução das avaliações" },
                    { new Guid("6708f65f-9b15-4abc-be4f-50ad12ecddfb"), "Distribuição das avaliações por período" },
                    { new Guid("7cdb8850-9a03-4690-a022-152bf0f205de"), "Número adequado de clusters de usuário" },
                    { new Guid("a4ecf6c8-bb59-4563-8cac-c1b7a865e54b"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("bb39c20c-4e30-46eb-8f5e-e7ef96d5c4c0"), "Avaliações de cada usuário por período" },
                    { new Guid("d02c4596-eed9-486f-8af3-34d13b1ffbd2"), "Frequência das avaliações por período de tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1fd87443-5cd8-4c68-a6b0-cf2948483f4a"), "Reviewer" },
                    { new Guid("381540ca-c456-4ea7-8263-4b3c0f2b92df"), "Admin" },
                    { new Guid("c54ef6c0-84cf-4c86-bf6a-f26af8523290"), "Researcher" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("50b93672-380b-4244-b99a-0ec5575c9d3b"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6708f65f-9b15-4abc-be4f-50ad12ecddfb"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("7cdb8850-9a03-4690-a022-152bf0f205de"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("a4ecf6c8-bb59-4563-8cac-c1b7a865e54b"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("bb39c20c-4e30-46eb-8f5e-e7ef96d5c4c0"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d02c4596-eed9-486f-8af3-34d13b1ffbd2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1fd87443-5cd8-4c68-a6b0-cf2948483f4a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("381540ca-c456-4ea7-8263-4b3c0f2b92df"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c54ef6c0-84cf-4c86-bf6a-f26af8523290"));

            migrationBuilder.AlterColumn<byte>(
                name: "PeriodType",
                table: "Projects",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(10)");

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
    }
}
