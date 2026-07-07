using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSiteAccesses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSiteAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSiteAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSiteAccesses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSiteAccesses_WeighbridgeSites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "WeighbridgeSites",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSiteAccesses_SiteId",
                table: "UserSiteAccesses",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSiteAccesses_UserId_SiteId",
                table: "UserSiteAccesses",
                columns: new[] { "UserId", "SiteId" },
                unique: true);

            migrationBuilder.Sql(@"
                INSERT INTO UserSiteAccesses (UserId, SiteId, AssignedAt)
                SELECT users.Id, users.SelectedSiteId, GETDATE()
                FROM AspNetUsers AS users
                INNER JOIN WeighbridgeSites AS sites
                    ON users.SelectedSiteId = sites.ID
                WHERE users.SelectedSiteId IS NOT NULL
                  AND NOT EXISTS (
                      SELECT 1
                      FROM UserSiteAccesses AS access
                      WHERE access.UserId = users.Id
                        AND access.SiteId = users.SelectedSiteId
                  )
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSiteAccesses");
        }
    }
}
