using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class Revamp_DB_Infastructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicleInformations_LicenseDetails_LicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicleInformations_NoLicenseDetails_NoLicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_LicenseDetails_LicenseId",
                table: "Violations");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_NoLicenseDetails_NoLicenseId",
                table: "Violations");

            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_LicenseDetails_LicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_NoLicenseDetails_NoLicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropTable(
                name: "NoLicenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_ViolatorTicketInformations_LicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropIndex(
                name: "IX_ViolatorTicketInformations_NoLicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropIndex(
                name: "IX_Violations_LicenseId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_Violations_NoLicenseId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_vehicleInformations_LicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropIndex(
                name: "IX_vehicleInformations_NoLicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropColumn(
                name: "NoLicenseId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "NoLicenseId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "Violator",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "NoLicenseId",
                table: "vehicleInformations");

            migrationBuilder.AddColumn<int>(
                name: "ViolatorId",
                table: "ViolatorTicketInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViolatorId",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViolatorId",
                table: "vehicleInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViolatorId",
                table: "LicenseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Violators",
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
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violators", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViolatorTicketInformations_ViolatorId",
                table: "ViolatorTicketInformations",
                column: "ViolatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_ViolatorId",
                table: "Violations",
                column: "ViolatorId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicleInformations_ViolatorId",
                table: "vehicleInformations",
                column: "ViolatorId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseDetails_ViolatorId",
                table: "LicenseDetails",
                column: "ViolatorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseDetails_Violators_ViolatorId",
                table: "LicenseDetails",
                column: "ViolatorId",
                principalTable: "Violators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicleInformations_Violators_ViolatorId",
                table: "vehicleInformations",
                column: "ViolatorId",
                principalTable: "Violators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_Violators_ViolatorId",
                table: "Violations",
                column: "ViolatorId",
                principalTable: "Violators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViolatorTicketInformations_Violators_ViolatorId",
                table: "ViolatorTicketInformations",
                column: "ViolatorId",
                principalTable: "Violators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicenseDetails_Violators_ViolatorId",
                table: "LicenseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicleInformations_Violators_ViolatorId",
                table: "vehicleInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_Violators_ViolatorId",
                table: "Violations");

            migrationBuilder.DropForeignKey(
                name: "FK_ViolatorTicketInformations_Violators_ViolatorId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropTable(
                name: "Violators");

            migrationBuilder.DropIndex(
                name: "IX_ViolatorTicketInformations_ViolatorId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropIndex(
                name: "IX_Violations_ViolatorId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_vehicleInformations_ViolatorId",
                table: "vehicleInformations");

            migrationBuilder.DropIndex(
                name: "IX_LicenseDetails_ViolatorId",
                table: "LicenseDetails");

            migrationBuilder.DropColumn(
                name: "ViolatorId",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "ViolatorId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "ViolatorId",
                table: "vehicleInformations");

            migrationBuilder.DropColumn(
                name: "ViolatorId",
                table: "LicenseDetails");

            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "ViolatorTicketInformations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoLicenseId",
                table: "ViolatorTicketInformations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "Violations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Violations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoLicenseId",
                table: "Violations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Violator",
                table: "Violations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "vehicleInformations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoLicenseId",
                table: "vehicleInformations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NoLicenseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoLicenseDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViolatorTicketInformations_LicenseId",
                table: "ViolatorTicketInformations",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ViolatorTicketInformations_NoLicenseId",
                table: "ViolatorTicketInformations",
                column: "NoLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_LicenseId",
                table: "Violations",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_NoLicenseId",
                table: "Violations",
                column: "NoLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicleInformations_LicenseId",
                table: "vehicleInformations",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicleInformations_NoLicenseId",
                table: "vehicleInformations",
                column: "NoLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicleInformations_LicenseDetails_LicenseId",
                table: "vehicleInformations",
                column: "LicenseId",
                principalTable: "LicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicleInformations_NoLicenseDetails_NoLicenseId",
                table: "vehicleInformations",
                column: "NoLicenseId",
                principalTable: "NoLicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_LicenseDetails_LicenseId",
                table: "Violations",
                column: "LicenseId",
                principalTable: "LicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_NoLicenseDetails_NoLicenseId",
                table: "Violations",
                column: "NoLicenseId",
                principalTable: "NoLicenseDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
