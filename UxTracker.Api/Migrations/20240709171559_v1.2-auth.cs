using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v12auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(32)",
                oldMaxLength: 32);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "NVARCHAR(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");
        }
    }
}
