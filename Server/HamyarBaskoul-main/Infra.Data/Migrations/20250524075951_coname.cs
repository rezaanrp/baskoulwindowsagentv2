using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class coname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "b30de92e-413b-4644-9903-f01bcc930861");

            migrationBuilder.UpdateData(
                table: "CodeMarkazs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoName",
                value: "شرکت بین‌المللی سیستم‌ها و اتوماسیون (ایریسا)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "0b90c098-aafc-4919-ab4a-50b3ebcb7b5e");

            migrationBuilder.UpdateData(
                table: "CodeMarkazs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoName",
                value: "irisa");
        }
    }
}
