using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_placeofvio_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AOR",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "AORExpiration",
                table: "vehicleInformations");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfViolation",
                table: "vehicleInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceOfViolation",
                table: "vehicleInformations");

            migrationBuilder.AddColumn<string>(
                name: "AOR",
                table: "vehicleInformations",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AORExpiration",
                table: "vehicleInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
