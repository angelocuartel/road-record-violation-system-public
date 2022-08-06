using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class AddLogHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersLogHistories",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeIn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TimeOut = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateIn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOut = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogHistories", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_UsersLogHistories_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersLogHistories_Id",
                table: "UsersLogHistories",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersLogHistories");
        }
    }
}
