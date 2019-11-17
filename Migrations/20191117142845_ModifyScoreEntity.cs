using Microsoft.EntityFrameworkCore.Migrations;

namespace TkdScoringApp.API.Migrations
{
    public partial class ModifyScoreEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Score",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Score");
        }
    }
}
