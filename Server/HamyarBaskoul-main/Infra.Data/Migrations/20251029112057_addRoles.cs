using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "e2ba1d81-be86-4e8f-8424-673bae5b75fd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "CodMarkaz", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abc12def-1234-2548-89ab-1234567890ab", null, "9bc3912d-d4de-465d-9d1e-3bc63d67e669", "NonHamyarAdmin", "NONHAMYAADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "CodMarkaz", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "abc12def-1234-8954-89ab-1234567890ab", null, "7371369f-5a40-4b3d-a953-2d71569b6551", "NonHamyarUser", "NONHAMYARUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-2548-89ab-1234567890ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-8954-89ab-1234567890ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "de3e4a22-c84b-4b69-8bc5-008b9c6984d6");
        }
    }
}

