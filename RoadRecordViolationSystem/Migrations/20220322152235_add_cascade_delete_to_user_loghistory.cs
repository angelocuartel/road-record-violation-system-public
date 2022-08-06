using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class add_cascade_delete_to_user_loghistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersLogHistories_AspNetUsers_Id",
                table: "UsersLogHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLogHistories_AspNetUsers_Id",
                table: "UsersLogHistories",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersLogHistories_AspNetUsers_Id",
                table: "UsersLogHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLogHistories_AspNetUsers_Id",
                table: "UsersLogHistories",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
