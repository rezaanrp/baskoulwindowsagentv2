using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class IPColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "UserSites",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "UserSites",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "Sites",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "Sites",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "ReportSettings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "ReportSettings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "ObjectTransactionTypeUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "ObjectTransactionTypeUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "ObjectFormUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "ObjectFormUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "ObjectForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "ObjectForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "Mabanis",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "Mabanis",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "GhabzSerialTrackers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "GhabzSerialTrackers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "CodeMarkazs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "CodeMarkazs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "baskouls",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "baskouls",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "BaseTables",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "BaseTables",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateIp",
                table: "BargeBaskouls",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyIp",
                table: "BargeBaskouls",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "c214e649-48c8-4148-a291-ee1f50a5945b");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "UserSites");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "UserSites");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "ReportSettings");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "ReportSettings");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "ObjectTransactionTypeUsers");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "ObjectTransactionTypeUsers");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "ObjectFormUsers");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "ObjectFormUsers");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "ObjectForms");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "ObjectForms");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "Mabanis");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "Mabanis");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "GhabzSerialTrackers");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "GhabzSerialTrackers");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "CodeMarkazs");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "CodeMarkazs");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "baskouls");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "baskouls");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "BaseTables");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "BaseTables");

            migrationBuilder.DropColumn(
                name: "CreateIp",
                table: "BargeBaskouls");

            migrationBuilder.DropColumn(
                name: "ModifyIp",
                table: "BargeBaskouls");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abc12def-1234-4567-89ab-1234567890ab",
                column: "ConcurrencyStamp",
                value: "c1785d77-b27b-479f-adb4-0d815abef16d");
        }
    }
}

