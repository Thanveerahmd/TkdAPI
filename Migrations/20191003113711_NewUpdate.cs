using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TkdScoringApp.API.Migrations
{
    public partial class NewUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Judge_Match_MatchId",
                table: "Judge");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "Player",
                newName: "Totalscore");

            migrationBuilder.RenameColumn(
                name: "foul",
                table: "Player",
                newName: "Totalfoul");

            migrationBuilder.AddColumn<int>(
                name: "NoOfJudges",
                table: "Match",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "Judge",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "kickbody",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    JudgeId = table.Column<int>(nullable: false),
                    NoOfConfirmation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kickbody", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kickhead",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    JudgeId = table.Column<int>(nullable: false),
                    NoOfConfirmation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kickhead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "punch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    JudgeId = table.Column<int>(nullable: false),
                    NoOfConfirmation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_punch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "turningKickBody",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    JudgeId = table.Column<int>(nullable: false),
                    NoOfConfirmation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turningKickBody", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "turningKickHead",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    JudgeId = table.Column<int>(nullable: false),
                    NoOfConfirmation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turningKickHead", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Judge_Match_MatchId",
                table: "Judge",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Judge_Match_MatchId",
                table: "Judge");

            migrationBuilder.DropTable(
                name: "kickbody");

            migrationBuilder.DropTable(
                name: "kickhead");

            migrationBuilder.DropTable(
                name: "punch");

            migrationBuilder.DropTable(
                name: "turningKickBody");

            migrationBuilder.DropTable(
                name: "turningKickHead");

            migrationBuilder.DropColumn(
                name: "NoOfJudges",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "Totalscore",
                table: "Player",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "Totalfoul",
                table: "Player",
                newName: "foul");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "Judge",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Judge_Match_MatchId",
                table: "Judge",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
