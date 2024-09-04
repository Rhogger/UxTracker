using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v10projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("62d51832-25cf-457d-b192-1c281de0c64d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("73bc5e43-a806-46c9-becf-f778e7031069"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("825533a5-e09d-431c-bd5a-750261e6b94e"));

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", maxLength: 80, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", maxLength: 80, nullable: false),
                    PeriodType = table.Column<byte>(type: "TINYINT", nullable: false),
                    Period = table.Column<byte>(type: "TINYINT", nullable: false),
                    ConsentTerm = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRelatory",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRelatory", x => new { x.ProjectId, x.RelatoryId });
                    table.ForeignKey(
                        name: "FK_ProjectRelatory_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRelatory_Relatories_RelatoryId",
                        column: x => x.RelatoryId,
                        principalTable: "Relatories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRelatory_RelatoryId",
                table: "ProjectRelatory",
                column: "RelatoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRelatory");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Relatories");

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("62d51832-25cf-457d-b192-1c281de0c64d"), "Reviewer" },
                    { new Guid("73bc5e43-a806-46c9-becf-f778e7031069"), "Admin" },
                    { new Guid("825533a5-e09d-431c-bd5a-750261e6b94e"), "Researcher" }
                });
        }
    }
}
