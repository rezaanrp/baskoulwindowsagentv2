using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class setting567 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeMarkaz",
                table: "Settings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "c1785d77-b27b-479f-adb4-0d815abef16d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeMarkaz",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "85b81a41-fe34-4be6-9849-c18918eef0fa");
        }
    }
}

