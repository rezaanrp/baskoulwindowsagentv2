using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SettingValue = table.Column<double>(type: "float", nullable: true),
                    SettingParameter = table.Column<int>(type: "int", nullable: true),
                    Sal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delet = table.Column<bool>(type: "bit", nullable: true),
                    Comp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Defaults = table.Column<bool>(type: "bit", nullable: true),
                    Karbar_Ins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Karbar_Up = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Ins = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Up = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tozihat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dore_id = table.Column<long>(type: "bigint", nullable: true),
                    api_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "4dc6cc82-cef8-4551-8850-f6025406d541");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "b5fd1505-bcec-4ec6-b9a1-038ea64b8ba6");
        }
    }
}
