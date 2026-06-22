using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class _5453 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "baskouls",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_baskouls_UserID",
                table: "baskouls",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_baskouls_AspNetUsers_UserID",
                table: "baskouls",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baskouls_AspNetUsers_UserID",
                table: "baskouls");

            migrationBuilder.DropIndex(
                name: "IX_baskouls_UserID",
                table: "baskouls");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "baskouls");
        }
    }
}

