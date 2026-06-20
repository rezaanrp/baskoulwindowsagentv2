using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "0b90c098-aafc-4919-ab4a-50b3ebcb7b5e");

            migrationBuilder.InsertData(
                table: "CodeMarkazs",
                columns: new[] { "Id", "APIURL", "AutoAsync", "CoName", "CodMarkaz", "CreateIp", "MarkazURL", "ModifyIp" },
                values: new object[] { 1, null, true, "irisa", "1", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CodeMarkazs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "14a55738-a397-4fba-b243-6f848e609d3d");
        }
    }
}
