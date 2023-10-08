using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departements",
                columns: table => new
                {
                    DID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departements", x => x.DID);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    SubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Period = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.SubID);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StuId);
                    table.ForeignKey(
                        name: "FK_students_departements_DID",
                        column: x => x.DID,
                        principalTable: "departements",
                        principalColumn: "DID");
                });

            migrationBuilder.CreateTable(
                name: "departementSubjects",
                columns: table => new
                {
                    DeptSubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DID = table.Column<int>(type: "int", nullable: true),
                    SubID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departementSubjects", x => x.DeptSubID);
                    table.ForeignKey(
                        name: "FK_departementSubjects_departements_DID",
                        column: x => x.DID,
                        principalTable: "departements",
                        principalColumn: "DID");
                    table.ForeignKey(
                        name: "FK_departementSubjects_subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "subjects",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateTable(
                name: "studentSubjects",
                columns: table => new
                {
                    StudSubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudID = table.Column<int>(type: "int", nullable: true),
                    SubID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentSubjects", x => x.StudSubID);
                    table.ForeignKey(
                        name: "FK_studentSubjects_students_StudID",
                        column: x => x.StudID,
                        principalTable: "students",
                        principalColumn: "StuId");
                    table.ForeignKey(
                        name: "FK_studentSubjects_subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "subjects",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_departementSubjects_DID",
                table: "departementSubjects",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_departementSubjects_SubID",
                table: "departementSubjects",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_students_DID",
                table: "students",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjects_StudID",
                table: "studentSubjects",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjects_SubID",
                table: "studentSubjects",
                column: "SubID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departementSubjects");

            migrationBuilder.DropTable(
                name: "studentSubjects");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "departements");
        }
    }
}
