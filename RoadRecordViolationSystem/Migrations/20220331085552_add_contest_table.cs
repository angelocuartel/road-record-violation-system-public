using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_contest_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolatorId = table.Column<int>(type: "int", nullable: false),
                    EnforcerId = table.Column<int>(type: "int", nullable: false),
                    TicketNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrCrImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContestLetterImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProofContestVideoImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgainstEnforcer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contests");
        }
    }
}
