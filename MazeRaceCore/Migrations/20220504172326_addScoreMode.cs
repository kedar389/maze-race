using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MazeRaceCore.Migrations
{
    public partial class addScoreMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Scores");
        }
    }
}
