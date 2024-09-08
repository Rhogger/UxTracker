using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v17auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivate",
                table: "Users",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivate",
                table: "Users");
        }
    }
}
