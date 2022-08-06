using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadRecordViolationSystem.Migrations
{
    public partial class ReconfigDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsersInformationTable_UsersInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInformationTable_AddressTable_AddressID",
                table: "UsersInformationTable");

            migrationBuilder.DropTable(
                name: "AddressTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInformationTable",
                table: "UsersInformationTable");

            migrationBuilder.DropIndex(
                name: "IX_UsersInformationTable_AddressID",
                table: "UsersInformationTable");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "UsersInformationTable");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UsersInformationTable");

            migrationBuilder.RenameTable(
                name: "UsersInformationTable",
                newName: "UsersInformations");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "UsersInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UsersInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UsersInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UsersInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInformations",
                table: "UsersInformations",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsersInformations_UsersInfoId",
                table: "AspNetUsers",
                column: "UsersInfoId",
                principalTable: "UsersInformations",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsersInformations_UsersInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInformations",
                table: "UsersInformations");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UsersInformations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UsersInformations");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UsersInformations");

            migrationBuilder.RenameTable(
                name: "UsersInformations",
                newName: "UsersInformationTable");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "UsersInformationTable",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "UsersInformationTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UsersInformationTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInformationTable",
                table: "UsersInformationTable",
                column: "UserInfoId");

            migrationBuilder.CreateTable(
                name: "AddressTable",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brgy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTable", x => x.AddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInformationTable_AddressID",
                table: "UsersInformationTable",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsersInformationTable_UsersInfoId",
                table: "AspNetUsers",
                column: "UsersInfoId",
                principalTable: "UsersInformationTable",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInformationTable_AddressTable_AddressID",
                table: "UsersInformationTable",
                column: "AddressID",
                principalTable: "AddressTable",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
