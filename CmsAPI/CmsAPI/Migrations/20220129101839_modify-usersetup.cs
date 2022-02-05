using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Migrations
{
    public partial class modifyusersetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationDate",
                table: "UserSetup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserSetup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "UserSetup",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "UserSetup",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UserSetup");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserSetup");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "UserSetup");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserSetup");
        }
    }
}
