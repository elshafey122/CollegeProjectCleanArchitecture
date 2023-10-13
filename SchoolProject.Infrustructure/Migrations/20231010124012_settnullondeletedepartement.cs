using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class settnullondeletedepartement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_departements_InsManager",
                table: "departements");

            migrationBuilder.CreateIndex(
                name: "IX_departements_InsManager",
                table: "departements",
                column: "InsManager");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_departements_InsManager",
                table: "departements");

            migrationBuilder.CreateIndex(
                name: "IX_departements_InsManager",
                table: "departements",
                column: "InsManager",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
