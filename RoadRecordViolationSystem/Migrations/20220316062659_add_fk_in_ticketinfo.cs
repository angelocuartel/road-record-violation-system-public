using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_fk_in_ticketinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "vehicleId",
                table: "ViolatorTicketInformations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ViolatorTicketInformations_vehicleId",
                table: "ViolatorTicketInformations",
                column: "vehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ViolatorTicketInformations_vehicleInformations_vehicleId",
                table: "ViolatorTicketInformations",
                column: "vehicleId",
                principalTable: "vehicleInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_vehicleInformations_vehicleId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropIndex(
                name: "IX_ViolatorTicketInformations_vehicleId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropColumn(
                name: "vehicleId",
                table: "ViolatorTicketInformations");
        }
    }
}
