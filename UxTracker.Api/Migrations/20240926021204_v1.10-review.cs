using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v110review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ReviewersCount",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    RatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Reviewers_UserId",
                        column: x => x.UserId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("230d81b7-e80a-4e07-aa24-0a0810e1b8b5"), "Avaliações de cada usuário por período" },
                    { new Guid("28a7a185-f704-4d5d-88e5-da6a643cbbbd"), "Visão geral da evolução das avaliações" },
                    { new Guid("4e5a8bdf-516d-4094-960d-9a7116725b97"), "Distribuição das avaliações por período" },
                    { new Guid("62c408cb-dc25-4209-b405-6befd5dcaa5d"), "Número adequado de clusters de usuário" },
                    { new Guid("d9a791fa-0d20-47bd-9683-88bf52b3bfc2"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("dbd22ca3-a9b4-495c-ab87-b69824d8fcc9"), "Frequência das avaliações por período de tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3e5bbb54-f4f9-4baf-8108-c341436921f9"), "Admin" },
                    { new Guid("ac43f877-bd0d-496d-bb21-2aed8f293ddc"), "Researcher" },
                    { new Guid("f40d2d9d-7185-4142-8b29-97fd4b76375c"), "Reviewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("230d81b7-e80a-4e07-aa24-0a0810e1b8b5"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("28a7a185-f704-4d5d-88e5-da6a643cbbbd"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4e5a8bdf-516d-4094-960d-9a7116725b97"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("62c408cb-dc25-4209-b405-6befd5dcaa5d"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("d9a791fa-0d20-47bd-9683-88bf52b3bfc2"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("dbd22ca3-a9b4-495c-ab87-b69824d8fcc9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e5bbb54-f4f9-4baf-8108-c341436921f9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ac43f877-bd0d-496d-bb21-2aed8f293ddc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f40d2d9d-7185-4142-8b29-97fd4b76375c"));

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
    }
}
