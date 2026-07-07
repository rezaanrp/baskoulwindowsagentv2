using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameOrganizationModelsToWeighbridgeDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baskouls_AspNetUsers_UserID",
                table: "baskouls");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSites_AspNetUsers_UserId",
                table: "UserSites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSites_Sites_SiteId",
                table: "UserSites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_baskouls",
                table: "baskouls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodeMarkazs",
                table: "CodeMarkazs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSites",
                table: "UserSites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.RenameTable(
                name: "baskouls",
                newName: "Weighbridges");

            migrationBuilder.RenameTable(
                name: "CodeMarkazs",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "UserSites",
                newName: "WeighbridgeSiteUsers");

            migrationBuilder.RenameTable(
                name: "Sites",
                newName: "WeighbridgeSites");

            migrationBuilder.RenameColumn(
                name: "Site",
                table: "Weighbridges",
                newName: "WeighbridgeSite");

            migrationBuilder.RenameColumn(
                name: "CodeMarkaz",
                table: "WeighbridgeSites",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "CodeMarkaz",
                table: "ReportSettings",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_baskouls_UserID",
                table: "Weighbridges",
                newName: "IX_Weighbridges_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserSites_SiteId",
                table: "WeighbridgeSiteUsers",
                newName: "IX_WeighbridgeSiteUsers_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSites_UserId",
                table: "WeighbridgeSiteUsers",
                newName: "IX_WeighbridgeSiteUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weighbridges",
                table: "Weighbridges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeighbridgeSiteUsers",
                table: "WeighbridgeSiteUsers",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeighbridgeSites",
                table: "WeighbridgeSites",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Weighbridges_AspNetUsers_UserID",
                table: "Weighbridges",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeighbridgeSiteUsers_AspNetUsers_UserId",
                table: "WeighbridgeSiteUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeighbridgeSiteUsers_WeighbridgeSites_SiteId",
                table: "WeighbridgeSiteUsers",
                column: "SiteId",
                principalTable: "WeighbridgeSites",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weighbridges_AspNetUsers_UserID",
                table: "Weighbridges");

            migrationBuilder.DropForeignKey(
                name: "FK_WeighbridgeSiteUsers_AspNetUsers_UserId",
                table: "WeighbridgeSiteUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_WeighbridgeSiteUsers_WeighbridgeSites_SiteId",
                table: "WeighbridgeSiteUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weighbridges",
                table: "Weighbridges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeighbridgeSiteUsers",
                table: "WeighbridgeSiteUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeighbridgeSites",
                table: "WeighbridgeSites");

            migrationBuilder.RenameColumn(
                name: "WeighbridgeSite",
                table: "Weighbridges",
                newName: "Site");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "WeighbridgeSites",
                newName: "CodeMarkaz");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "ReportSettings",
                newName: "CodeMarkaz");

            migrationBuilder.RenameTable(
                name: "Weighbridges",
                newName: "baskouls");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "CodeMarkazs");

            migrationBuilder.RenameTable(
                name: "WeighbridgeSiteUsers",
                newName: "UserSites");

            migrationBuilder.RenameTable(
                name: "WeighbridgeSites",
                newName: "Sites");

            migrationBuilder.RenameIndex(
                name: "IX_Weighbridges_UserID",
                table: "baskouls",
                newName: "IX_baskouls_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_WeighbridgeSiteUsers_SiteId",
                table: "UserSites",
                newName: "IX_UserSites_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_WeighbridgeSiteUsers_UserId",
                table: "UserSites",
                newName: "IX_UserSites_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_baskouls",
                table: "baskouls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodeMarkazs",
                table: "CodeMarkazs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSites",
                table: "UserSites",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_baskouls_AspNetUsers_UserID",
                table: "baskouls",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSites_AspNetUsers_UserId",
                table: "UserSites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSites_Sites_SiteId",
                table: "UserSites",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
