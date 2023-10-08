using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class addnullablefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departements_instructors_InsManager",
                table: "departements");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_departements_InsManager",
                table: "departements");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "subjects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "grade",
                table: "studentSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "instructors",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DId",
                table: "instructors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "InsManager",
                table: "departements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_departements_InsManager",
                table: "departements",
                column: "InsManager",
                unique: true,
                filter: "[InsManager] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_departements_instructors_InsManager",
                table: "departements",
                column: "InsManager",
                principalTable: "instructors",
                principalColumn: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departements_instructors_InsManager",
                table: "departements");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_departements_InsManager",
                table: "departements");

            migrationBuilder.DropColumn(
                name: "grade",
                table: "studentSubjects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "instructors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DId",
                table: "instructors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InsManager",
                table: "departements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_departements_InsManager",
                table: "departements",
                column: "InsManager",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_departements_instructors_InsManager",
                table: "departements",
                column: "InsManager",
                principalTable: "instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
