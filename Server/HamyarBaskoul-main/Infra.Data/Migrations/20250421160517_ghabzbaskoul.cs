using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class ghabzbaskoul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GhabzSerialTrackers",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    CodMarkaz = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Serial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GhabzSerialTrackers", x => new { x.CodMarkaz, x.Year });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GhabzSerialTrackers");
        }
    }
}

