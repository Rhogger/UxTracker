using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v181auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
