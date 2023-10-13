using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_students_departements_DID",
                table: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_students_departements_DID",
                table: "students",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_students_departements_DID",
                table: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_students_departements_DID",
                table: "students",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
