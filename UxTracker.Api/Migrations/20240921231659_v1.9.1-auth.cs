using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UxTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class v191auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Researchers_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Reviewers_UserId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("01666c38-a647-4f1c-b1c2-0c813c613840"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("2c4c2af0-d720-4058-9fef-953d17c92fd0"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("5547634f-6d02-4ca7-9a42-1dd10a5f0c07"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("6dfaa091-b10f-49ee-92b1-e1823f1a7bf8"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("997af5c3-9a33-48d2-b652-6a230e967d47"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("c3b150ad-8201-403a-a50a-55c1d83ae455"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1464fec5-767d-42df-b7b9-ce7ca72ba876"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dd60540-56fd-4ba4-a1f7-c48eb2da9b09"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a1f008c5-64a9-4ee3-a782-7fbdee351784"));

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("4ad8525f-3f58-433b-9fb3-8c814fc815b5"), "Frequência das avaliações por período de tempo" },
                    { new Guid("83b45c3c-0610-453a-8f16-5b9abad7d4ff"), "Média da experiência do usuário ao longo do tempo" },
                    { new Guid("91ff8f56-4c20-48da-92b4-dc9a4156cd51"), "Número adequado de clusters de usuário" },
                    { new Guid("de79a69f-7208-4d35-ae6c-85c0f33d27e2"), "Visão geral da evolução das avaliações" },
                    { new Guid("ded56866-96fc-4565-a5bc-c869133e428c"), "Distribuição das avaliações por período" },
                    { new Guid("ebf42bbd-60d9-4249-9902-2703490dc23d"), "Avaliações de cada usuário por período" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01323dfa-a085-4ee2-807c-504de0e1638e"), "Reviewer" },
                    { new Guid("5de19297-30d3-4bb6-81c4-5a3c041855b8"), "Admin" },
                    { new Guid("95814c9d-fe0e-46e4-957c-5c0a6b037c89"), "Researcher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Researchers_Id",
                table: "UserRole",
                column: "Id",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Reviewers_Id",
                table: "UserRole",
                column: "Id",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Researchers_Id",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Reviewers_Id",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole");

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("4ad8525f-3f58-433b-9fb3-8c814fc815b5"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("83b45c3c-0610-453a-8f16-5b9abad7d4ff"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("91ff8f56-4c20-48da-92b4-dc9a4156cd51"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("de79a69f-7208-4d35-ae6c-85c0f33d27e2"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ded56866-96fc-4565-a5bc-c869133e428c"));

            migrationBuilder.DeleteData(
                table: "Relatories",
                keyColumn: "Id",
                keyValue: new Guid("ebf42bbd-60d9-4249-9902-2703490dc23d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("01323dfa-a085-4ee2-807c-504de0e1638e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5de19297-30d3-4bb6-81c4-5a3c041855b8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("95814c9d-fe0e-46e4-957c-5c0a6b037c89"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.InsertData(
                table: "Relatories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("01666c38-a647-4f1c-b1c2-0c813c613840"), "Avaliações de cada usuário por período" },
                    { new Guid("2c4c2af0-d720-4058-9fef-953d17c92fd0"), "Distribuição das avaliações por período" },
                    { new Guid("5547634f-6d02-4ca7-9a42-1dd10a5f0c07"), "Frequência das avaliações por período de tempo" },
                    { new Guid("6dfaa091-b10f-49ee-92b1-e1823f1a7bf8"), "Visão geral da evolução das avaliações" },
                    { new Guid("997af5c3-9a33-48d2-b652-6a230e967d47"), "Número adequado de clusters de usuário" },
                    { new Guid("c3b150ad-8201-403a-a50a-55c1d83ae455"), "Média da experiência do usuário ao longo do tempo" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1464fec5-767d-42df-b7b9-ce7ca72ba876"), "Admin" },
                    { new Guid("3dd60540-56fd-4ba4-a1f7-c48eb2da9b09"), "Researcher" },
                    { new Guid("a1f008c5-64a9-4ee3-a782-7fbdee351784"), "Reviewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Researchers_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Reviewers_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
