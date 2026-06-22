using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class reportsetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShomareSanad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShomareBaznegari = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TarikhBaznegari = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FactoryAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LogoPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FactoryPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfficePhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telfax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KarbarIns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KarbarUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIns = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSettings", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportSettings");
        }
    }
}

