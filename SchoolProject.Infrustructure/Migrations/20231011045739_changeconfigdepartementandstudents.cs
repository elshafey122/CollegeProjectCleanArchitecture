using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class changeconfigdepartementandstudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_departements_DID",
                table: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_students_departements_DID",
                table: "students",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_departements_DID",
                table: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_students_departements_DID",
                table: "students",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
