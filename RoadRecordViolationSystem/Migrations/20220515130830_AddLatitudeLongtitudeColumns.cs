using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class AddLatitudeLongtitudeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "vehicleInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "longtitude",
                table: "vehicleInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "longtitude",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "longtitude",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "longtitude",
                table: "Accidents");
        }
    }
}
