using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databaze_tabulky.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HighschoolApplications",
                columns: table => new
                {
                    HighschoolApplicationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighschoolApplications", x => x.HighschoolApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "Highschools",
                columns: table => new
                {
                    HighSchoolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highschools", x => x.HighSchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Jmeno = table.Column<string>(type: "TEXT", nullable: false),
                    Prijmeni = table.Column<string>(type: "TEXT", nullable: false),
                    RodneCislo = table.Column<string>(type: "TEXT", nullable: false),
                    DatumNarozeni = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Adresa = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Mesto = table.Column<string>(type: "TEXT", nullable: false),
                    PSC = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "StudyFieldApplications",
                columns: table => new
                {
                    StudyFieldApplicationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HighschoolApplicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyFieldId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFieldApplications", x => x.StudyFieldApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "StudyFields",
                columns: table => new
                {
                    StudyFieldId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HighSchoolId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudyFieldCode = table.Column<string>(type: "TEXT", nullable: false),
                    StudyName = table.Column<string>(type: "TEXT", nullable: false),
                    StudyDesc = table.Column<string>(type: "TEXT", nullable: false),
                    StudySlots = table.Column<int>(type: "INTEGER", nullable: false),
                    Graduation = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFields", x => x.StudyFieldId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighschoolApplications");

            migrationBuilder.DropTable(
                name: "Highschools");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "StudyFieldApplications");

            migrationBuilder.DropTable(
                name: "StudyFields");
        }
    }
}
