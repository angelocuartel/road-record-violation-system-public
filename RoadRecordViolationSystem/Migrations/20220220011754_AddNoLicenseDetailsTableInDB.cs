using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class AddNoLicenseDetailsTableInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoLicenseId",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoLicenseId",
                table: "vehicleInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NoLicenseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GivenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoLicenseDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Violations_NoLicenseId",
                table: "Violations",
                column: "NoLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicleInformations_NoLicenseId",
                table: "vehicleInformations",
                column: "NoLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicleInformations_NoLicenseDetails_NoLicenseId",
                table: "vehicleInformations",
                column: "NoLicenseId",
                principalTable: "NoLicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_NoLicenseDetails_NoLicenseId",
                table: "Violations",
                column: "NoLicenseId",
                principalTable: "NoLicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicleInformations_NoLicenseDetails_NoLicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_NoLicenseDetails_NoLicenseId",
                table: "Violations");

            migrationBuilder.DropTable(
                name: "NoLicenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_Violations_NoLicenseId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_vehicleInformations_NoLicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "NoLicenseId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "NoLicenseId",
                table: "vehicleInformations");
        }
    }
}
