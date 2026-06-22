using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newrameworkv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-2548-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "55a33924-8173-4f79-b41e-d438d1eada2a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "39a9015e-1958-405b-a4ea-25bf2249e783");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-8954-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "ae356140-5de4-4a67-b818-e1021aeacfcf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

