using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyUserRolesToAdminAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO AspNetUserRoles (UserId, RoleId)
                SELECT DISTINCT oldRoles.UserId, 'abc12def-1234-4567-89ab-1234567890ab'
                FROM AspNetUserRoles oldRoles
                WHERE oldRoles.RoleId IN (
                    'cbc43a8e-f7bb-4445-baaf-1add431ffbbf',
                    'abc12def-1234-2548-89ab-1234567890ab'
                )
                AND NOT EXISTS (
                    SELECT 1
                    FROM AspNetUserRoles userRole
                    WHERE userRole.UserId = oldRoles.UserId
                      AND userRole.RoleId = 'abc12def-1234-4567-89ab-1234567890ab'
                );

                INSERT INTO AspNetUserRoles (UserId, RoleId)
                SELECT DISTINCT oldRoles.UserId, 'cac43a6e-f7bb-4448-baaf-1add431ccbbf'
                FROM AspNetUserRoles oldRoles
                WHERE oldRoles.RoleId IN (
                    'abc12def-1234-8954-89ab-1234567890ab'
                )
                AND NOT EXISTS (
                    SELECT 1
                    FROM AspNetUserRoles userRole
                    WHERE userRole.UserId = oldRoles.UserId
                      AND userRole.RoleId = 'cac43a6e-f7bb-4448-baaf-1add431ccbbf'
                );

                DELETE FROM AspNetUserRoles
                WHERE RoleId IN (
                    'cbc43a8e-f7bb-4445-baaf-1add431ffbbf',
                    'abc12def-1234-2548-89ab-1234567890ab',
                    'abc12def-1234-8954-89ab-1234567890ab'
                );
                """);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-2548-89ab-1234567890ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-8954-89ab-1234567890ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "CodMarkaz", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abc12def-1234-2548-89ab-1234567890ab", null, "55a33924-8173-4f79-b41e-d438d1eada2a", "NonHamyarAdmin", "NONHAMYARADMIN" },
                    { "abc12def-1234-8954-89ab-1234567890ab", null, "ae356140-5de4-4a67-b818-e1021aeacfcf", "NonHamyarUser", "NONHAMYARUSER" },
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", null, "dde4cd0a-c55c-4c1b-874d-8d2e33c0c7eb", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
