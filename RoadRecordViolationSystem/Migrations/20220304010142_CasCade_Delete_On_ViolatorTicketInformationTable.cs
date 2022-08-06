using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class CasCade_Delete_On_ViolatorTicketInformationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_LicenseDetails_LicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_NoLicenseDetails_NoLicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.AddForeignKey(
                name: "FK_ViolatorTicketInformations_LicenseDetails_LicenseId",
                table: "ViolatorTicketInformations",
                column: "LicenseId",
                principalTable: "LicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViolatorTicketInformations_NoLicenseDetails_NoLicenseId",
                table: "ViolatorTicketInformations",
                column: "NoLicenseId",
                principalTable: "NoLicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_LicenseDetails_LicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_NoLicenseDetails_NoLicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.AddForeignKey(
                name: "FK_ViolatorTicketInformations_LicenseDetails_LicenseId",
                table: "ViolatorTicketInformations",
                column: "LicenseId",
                principalTable: "LicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ViolatorTicketInformations_NoLicenseDetails_NoLicenseId",
                table: "ViolatorTicketInformations",
                column: "NoLicenseId",
                principalTable: "NoLicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
