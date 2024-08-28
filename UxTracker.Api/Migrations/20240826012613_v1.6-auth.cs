using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v16auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetChangedAt",
                table: "Users",
                newName: "PasswordResetVerifiedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetVerifiedAt",
                table: "Users",
                newName: "PasswordResetChangedAt");
        }
    }
}
