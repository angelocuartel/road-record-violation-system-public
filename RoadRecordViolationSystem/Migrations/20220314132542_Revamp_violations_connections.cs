using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class Revamp_violations_connections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Violations_Violators_ViolatorId",
                table: "Violations");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "ViolatorTicketInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ViolatorId",
                table: "Violations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Violations_TicketId",
                table: "Violations",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_Violators_ViolatorId",
                table: "Violations",
                column: "ViolatorId",
                principalTable: "Violators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_ViolatorTicketInformations_TicketId",
                table: "Violations",
                column: "TicketId",
                principalTable: "ViolatorTicketInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Violations_Violators_ViolatorId",
                table: "Violations");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_ViolatorTicketInformations_TicketId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_Violations_TicketId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "ViolatorTicketInformations");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Violations");

            migrationBuilder.AlterColumn<int>(
                name: "ViolatorId",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_Violators_ViolatorId",
                table: "Violations",
                column: "ViolatorId",
                principalTable: "Violators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
