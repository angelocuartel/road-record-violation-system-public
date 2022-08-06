using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_new_column_in_violatorticketinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CommencingDateForAdditional",
                table: "ViolatorTicketInformations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommencingDateForAdditional",
                table: "ViolatorTicketInformations");
        }
    }
}
