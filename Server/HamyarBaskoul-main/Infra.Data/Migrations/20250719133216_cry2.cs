using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class cry2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "ReportSettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "de3e4a22-c84b-4b69-8bc5-008b9c6984d6");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "Sites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "ReportSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "e91b8657-01d9-4622-89f1-c0e7e8c7ebef");
        }
    }
}

