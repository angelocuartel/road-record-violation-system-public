using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_contest_sched : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Contests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ContestSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestApplicationId = table.Column<int>(type: "int", nullable: false),
                    Mediator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HearingScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HearingScheduleTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContestSchedules_Contests_ContestApplicationId",
                        column: x => x.ContestApplicationId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestSchedules_ContestApplicationId",
                table: "ContestSchedules",
                column: "ContestApplicationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestSchedules");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Contests");
        }
    }
}
