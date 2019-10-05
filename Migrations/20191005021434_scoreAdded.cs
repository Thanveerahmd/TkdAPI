using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TkdScoringApp.API.Migrations
{
    public partial class scoreAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfConsecutiveTaps",
                table: "turningKickHead",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfConsecutiveTaps",
                table: "turningKickBody",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfConsecutiveTaps",
                table: "punch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfConsecutiveTaps",
                table: "kickhead",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfConsecutiveTaps",
                table: "kickbody",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    ScoreValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropColumn(
                name: "NoOfConsecutiveTaps",
                table: "turningKickHead");

            migrationBuilder.DropColumn(
                name: "NoOfConsecutiveTaps",
                table: "turningKickBody");

            migrationBuilder.DropColumn(
                name: "NoOfConsecutiveTaps",
                table: "punch");

            migrationBuilder.DropColumn(
                name: "NoOfConsecutiveTaps",
                table: "kickhead");

            migrationBuilder.DropColumn(
                name: "NoOfConsecutiveTaps",
                table: "kickbody");
        }
    }
}
