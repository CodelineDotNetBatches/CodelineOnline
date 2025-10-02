using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Instructors",
                schema: "users",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    GithubUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Years_of_Experience = table.Column<int>(type: "int", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    InstructorCV = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Experience_Level = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Teaching_Style = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Specialization = table.Column<int>(type: "int", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                schema: "users",
                columns: table => new
                {
                    UID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                schema: "users",
                columns: table => new
                {
                    avilabilityId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    Avail_Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    day_of_week = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => new { x.InstructorId, x.avilabilityId });
                    table.ForeignKey(
                        name: "FK_Availabilities_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalSchema: "users",
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Instructors",
                columns: new[] { "InstructorId", "Experience_Level", "GithubUserName", "InstructorCV", "ProfileImage", "Specialization", "Teaching_Style", "Years_of_Experience" },
                values: new object[,]
                {
                    { 101, 1, "aliceGH", "https://example.com/cv/alice.pdf", "https://example.com/images/alice.jpg", 0, 0, 5 },
                    { 102, 2, "bobDev", "https://example.com/cv/bob.pdf", "https://example.com/images/bob.jpg", 4, 1, 8 },
                    { 103, 0, "carolCode", "https://example.com/cv/carol.pdf", "https://example.com/images/carol.jpg", 5, 2, 3 },
                    { 104, 2, "daveTech", "https://example.com/cv/dave.pdf", "https://example.com/images/dave.jpg", 7, 3, 10 }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Availabilities",
                columns: new[] { "InstructorId", "avilabilityId", "Avail_Status", "day_of_week", "time" },
                values: new object[,]
                {
                    { 101, 1, 1, 1, new TimeOnly(9, 0, 0) },
                    { 101, 2, 3, 3, new TimeOnly(14, 0, 0) },
                    { 102, 3, 2, 2, new TimeOnly(11, 0, 0) },
                    { 102, 4, 4, 4, new TimeOnly(16, 0, 0) },
                    { 103, 5, 1, 5, new TimeOnly(10, 0, 0) },
                    { 104, 6, 3, 6, new TimeOnly(13, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_Avail_Status",
                schema: "users",
                table: "Availabilities",
                column: "Avail_Status");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_GithubUserName",
                schema: "users",
                table: "Instructors",
                column: "GithubUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Specialization",
                schema: "users",
                table: "Instructors",
                column: "Specialization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availabilities",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Trainees",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Instructors",
                schema: "users");
        }
    }
}
