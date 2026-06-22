using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class userRole21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "b8fa56d8-4ba3-4578-a438-47c351ae44c2", "SuperAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "67dea7d7-4f5b-4855-b734-67701e403481", "SuoerAdmin" });
        }
    }
}

