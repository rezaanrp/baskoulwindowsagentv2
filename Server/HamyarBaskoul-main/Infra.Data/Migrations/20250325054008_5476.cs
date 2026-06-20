using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class _5476 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "RebuildAbilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "ObjectTransactionTypeUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "ObjectFormUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "ObjectForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "Mabanis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "DesignExecutionEmployerVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "DesignExecutionEmployers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "DesignExecutionEmployerFollowUps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "DataProtectionKeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BuilderProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BuilderProfileOngoingAndUpcomingProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BuilderProfileFollowUpResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BuilderProfileEstimatedNeeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BuilderProfileCompletedProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "baskouls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BaseTables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "BargeBaskouls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetUserTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetUserRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetUserLogins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetUserClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodMarkaz",
                table: "AspNetRoleClaims",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "RebuildAbilities");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "ObjectTransactionTypeUsers");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "ObjectFormUsers");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "ObjectForms");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "Mabanis");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "DesignExecutionEmployerVisits");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "DesignExecutionEmployers");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "DesignExecutionEmployerFollowUps");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "DataProtectionKeys");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BuilderProfiles");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BuilderProfileOngoingAndUpcomingProjects");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BuilderProfileFollowUpResults");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BuilderProfileEstimatedNeeds");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BuilderProfileCompletedProjects");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "baskouls");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BaseTables");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "BargeBaskouls");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CodMarkaz",
                table: "AspNetRoleClaims");
        }
    }
}
