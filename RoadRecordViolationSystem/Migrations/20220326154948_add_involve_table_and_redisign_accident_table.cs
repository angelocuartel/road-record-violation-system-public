using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_involve_table_and_redisign_accident_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNo",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Accidents");

            migrationBuilder.AddColumn<string>(
                name: "AccidentImage",
                table: "Accidents",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccidentInvolves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GivenName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    EmergencyContactNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentInvolves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccidentInvolves_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccidentInvolves_AccidentId",
                table: "AccidentInvolves",
                column: "AccidentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentInvolves");

            migrationBuilder.DropColumn(
                name: "AccidentImage",
                table: "Accidents");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Accidents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Accidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Accidents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNo",
                table: "Accidents",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Accidents",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "Accidents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Accidents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Accidents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
