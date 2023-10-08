using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class editpropperiod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departements_instructors_InsManager",
                table: "departements");

            migrationBuilder.DropForeignKey(
                name: "FK_departementSubjects_departements_DID",
                table: "departementSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_departementSubjects_subjects_SubID",
                table: "departementSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_instructorSubjects_instructors_InsId",
                table: "instructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_instructorSubjects_subjects_SubId",
                table: "instructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_students_departements_DID",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_studentSubjects_students_StudID",
                table: "studentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_studentSubjects_subjects_SubID",
                table: "studentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_departements_InsManager",
                table: "departements");

            migrationBuilder.AlterColumn<string>(
                name: "SubNameEn",
                table: "subjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubNameAr",
                table: "subjects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Period",
                table: "subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_departementSubjects_departements_DID",
                table: "departementSubjects",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_departementSubjects_subjects_SubID",
                table: "departementSubjects",
                column: "SubID",
                principalTable: "subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_instructorSubjects_instructors_InsId",
                table: "instructorSubjects",
                column: "InsId",
                principalTable: "instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_instructorSubjects_subjects_SubId",
                table: "instructorSubjects",
                column: "SubId",
                principalTable: "subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_students_departements_DID",
                table: "students",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_studentSubjects_students_StudID",
                table: "studentSubjects",
                column: "StudID",
                principalTable: "students",
                principalColumn: "StuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_studentSubjects_subjects_SubID",
                table: "studentSubjects",
                column: "SubID",
                principalTable: "subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departements_instructors_InsManager",
                table: "departements");

            migrationBuilder.DropForeignKey(
                name: "FK_departementSubjects_departements_DID",
                table: "departementSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_departementSubjects_subjects_SubID",
                table: "departementSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_instructorSubjects_instructors_InsId",
                table: "instructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_instructorSubjects_subjects_SubId",
                table: "instructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_students_departements_DID",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_studentSubjects_students_StudID",
                table: "studentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_studentSubjects_subjects_SubID",
                table: "studentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_departements_InsManager",
                table: "departements");

            migrationBuilder.AlterColumn<string>(
                name: "SubNameEn",
                table: "subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubNameAr",
                table: "subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "subjects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_departementSubjects_departements_DID",
                table: "departementSubjects",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departementSubjects_subjects_SubID",
                table: "departementSubjects",
                column: "SubID",
                principalTable: "subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_departements_DId",
                table: "instructors",
                column: "DId",
                principalTable: "departements",
                principalColumn: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_instructorSubjects_instructors_InsId",
                table: "instructorSubjects",
                column: "InsId",
                principalTable: "instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_instructorSubjects_subjects_SubId",
                table: "instructorSubjects",
                column: "SubId",
                principalTable: "subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_departements_DID",
                table: "students",
                column: "DID",
                principalTable: "departements",
                principalColumn: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_studentSubjects_students_StudID",
                table: "studentSubjects",
                column: "StudID",
                principalTable: "students",
                principalColumn: "StuId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_studentSubjects_subjects_SubID",
                table: "studentSubjects",
                column: "SubID",
                principalTable: "subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
