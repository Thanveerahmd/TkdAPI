using Microsoft.EntityFrameworkCore.Migrations;

namespace TkdScoringApp.API.Migrations
{
    public partial class matchPropsEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RingId",
                table: "Match",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isFinished",
                table: "Match",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RingId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "isFinished",
                table: "Match");
        }
    }
}
