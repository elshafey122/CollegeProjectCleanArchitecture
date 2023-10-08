using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class InitLocalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubName",
                table: "subjects",
                newName: "SubNameEn");

            migrationBuilder.RenameColumn(
                name: "StuName",
                table: "students",
                newName: "StuNameEn");

            migrationBuilder.RenameColumn(
                name: "DName",
                table: "departements",
                newName: "DNameEn");

            migrationBuilder.AddColumn<string>(
                name: "SubNameAr",
                table: "subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StuNameAr",
                table: "students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNameAr",
                table: "departements",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubNameAr",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "StuNameAr",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DNameAr",
                table: "departements");

            migrationBuilder.RenameColumn(
                name: "SubNameEn",
                table: "subjects",
                newName: "SubName");

            migrationBuilder.RenameColumn(
                name: "StuNameEn",
                table: "students",
                newName: "StuName");

            migrationBuilder.RenameColumn(
                name: "DNameEn",
                table: "departements",
                newName: "DName");
        }
    }
}
