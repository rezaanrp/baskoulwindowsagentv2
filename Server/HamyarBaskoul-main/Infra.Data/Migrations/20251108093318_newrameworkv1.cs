using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newrameworkv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-2548-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "ed2bde67-7078-46cb-a4b7-57d85f809527");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "cec0a675-c87b-4e06-b07a-a1fed361ac03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-8954-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "e15c12ca-2888-46f6-846c-0ceabc8d5ca8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-2548-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "9bc3912d-d4de-465d-9d1e-3bc63d67e669");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "e2ba1d81-be86-4e8f-8424-673bae5b75fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-8954-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "7371369f-5a40-4b3d-a953-2d71569b6551");
        }
    }
}

