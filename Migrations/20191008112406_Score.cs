using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TkdScoringApp.API.Migrations
{
    public partial class Score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "time",
                table: "Score",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                table: "Score");
        }
    }
}
