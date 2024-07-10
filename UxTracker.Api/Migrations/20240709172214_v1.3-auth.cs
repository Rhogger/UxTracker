using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v13auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "NVARCHAR(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(75)",
                oldMaxLength: 75);
        }
    }
}
