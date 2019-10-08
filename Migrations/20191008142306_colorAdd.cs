using Microsoft.EntityFrameworkCore.Migrations;

namespace TkdScoringApp.API.Migrations
{
    public partial class colorAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Player",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Player");
        }
    }
}
