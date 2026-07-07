using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSiteUserJoinAndAddOrganizationForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weighbridges_AspNetUsers_UserID",
                table: "Weighbridges");

            migrationBuilder.DropTable(
                name: "WeighbridgeSiteUsers");

            migrationBuilder.DropIndex(
                name: "IX_Weighbridges_UserID",
                table: "Weighbridges");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Weighbridges");

            migrationBuilder.RenameColumn(
                name: "WeighbridgeSite",
                table: "Weighbridges",
                newName: "WeighbridgeSiteId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "WeighbridgeSites",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE sites
                SET CompanyId = companies.Id
                FROM WeighbridgeSites AS sites
                INNER JOIN Companies AS companies
                    ON sites.Company = companies.CodMarkaz
            ");

            migrationBuilder.Sql(@"
                UPDATE sites
                SET CompanyId = fallback.Id
                FROM WeighbridgeSites AS sites
                CROSS APPLY (
                    SELECT TOP 1 Id
                    FROM Companies
                    ORDER BY Id
                ) AS fallback
                WHERE sites.CompanyId IS NULL
            ");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "WeighbridgeSites",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "Company",
                table: "WeighbridgeSites");

            migrationBuilder.CreateIndex(
                name: "IX_WeighbridgeSites_CompanyId",
                table: "WeighbridgeSites",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Weighbridges_WeighbridgeSiteId",
                table: "Weighbridges",
                column: "WeighbridgeSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weighbridges_WeighbridgeSites_WeighbridgeSiteId",
                table: "Weighbridges",
                column: "WeighbridgeSiteId",
                principalTable: "WeighbridgeSites",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeighbridgeSites_Companies_CompanyId",
                table: "WeighbridgeSites",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weighbridges_WeighbridgeSites_WeighbridgeSiteId",
                table: "Weighbridges");

            migrationBuilder.DropForeignKey(
                name: "FK_WeighbridgeSites_Companies_CompanyId",
                table: "WeighbridgeSites");

            migrationBuilder.DropIndex(
                name: "IX_WeighbridgeSites_CompanyId",
                table: "WeighbridgeSites");

            migrationBuilder.DropIndex(
                name: "IX_Weighbridges_WeighbridgeSiteId",
                table: "Weighbridges");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WeighbridgeSites");

            migrationBuilder.RenameColumn(
                name: "WeighbridgeSiteId",
                table: "Weighbridges",
                newName: "WeighbridgeSite");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "WeighbridgeSites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Weighbridges",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WeighbridgeSiteUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeighbridgeSiteUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeighbridgeSiteUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeighbridgeSiteUsers_WeighbridgeSites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "WeighbridgeSites",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weighbridges_UserID",
                table: "Weighbridges",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WeighbridgeSiteUsers_SiteId",
                table: "WeighbridgeSiteUsers",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WeighbridgeSiteUsers_UserId",
                table: "WeighbridgeSiteUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weighbridges_AspNetUsers_UserID",
                table: "Weighbridges",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
