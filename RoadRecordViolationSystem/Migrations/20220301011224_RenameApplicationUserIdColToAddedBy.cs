using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class RenameApplicationUserIdColToAddedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "ViolationTypes",
                newName: "AddedBy");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "NoLicenseDetails",
                newName: "AddedBy");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "LicenseDetails",
                newName: "AddedBy");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "Accidents",
                newName: "AddedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "ViolationTypes",
                newName: "applicationUserId");

            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "NoLicenseDetails",
                newName: "applicationUserId");

            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "LicenseDetails",
                newName: "applicationUserId");

            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "Accidents",
                newName: "applicationUserId");
        }
    }
}
