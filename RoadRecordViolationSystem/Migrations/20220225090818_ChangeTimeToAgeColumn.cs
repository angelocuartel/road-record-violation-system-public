using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class ChangeTimeToAgeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Accidents");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Accidents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Accidents");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
