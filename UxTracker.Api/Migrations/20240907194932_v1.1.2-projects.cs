using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v112projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ReviewersCount",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ReviewersCount",
                table: "Projects");

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
    }
}
