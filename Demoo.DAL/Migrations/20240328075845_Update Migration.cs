using Microsoft.EntityFrameworkCore.Migrations;

namespace Demoo.DAL.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FName");

            migrationBuilder.AddColumn<bool>(
                name: "isAgree",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAgree",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FName",
                table: "AspNetUsers",
                newName: "FirstName");
        }
    }
}
