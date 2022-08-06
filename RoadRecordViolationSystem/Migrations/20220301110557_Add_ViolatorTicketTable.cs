using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class Add_ViolatorTicketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViolatorTicketInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoLicenseId = table.Column<int>(type: "int", nullable: true),
                    LicenseId = table.Column<int>(type: "int", nullable: true),
                    ViolationCount = table.Column<int>(type: "int", nullable: false),
                    TotalAmountToBePayed = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrintedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePrinted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QrSecurityHash = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TicketNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViolatorTicketInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViolatorTicketInformations_LicenseDetails_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViolatorTicketInformations_NoLicenseDetails_NoLicenseId",
                        column: x => x.NoLicenseId,
                        principalTable: "NoLicenseDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViolatorTicketInformations_LicenseId",
                table: "ViolatorTicketInformations",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ViolatorTicketInformations_NoLicenseId",
                table: "ViolatorTicketInformations",
                column: "NoLicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViolatorTicketInformations");
        }
    }
}
