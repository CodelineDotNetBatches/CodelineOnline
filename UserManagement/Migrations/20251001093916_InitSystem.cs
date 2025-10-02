using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Batches",
                schema: "users",
                columns: table => new
                {
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timeline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "users",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    MonthsOfExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                schema: "users",
                columns: table => new
                {
                    TraineeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GithubUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationalBackground = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearningObjectives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraineeCV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                });

            migrationBuilder.CreateTable(
                name: "BatchTrainees",
                schema: "users",
                columns: table => new
                {
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TraineeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchTrainees", x => new { x.BatchId, x.TraineeId });
                    table.ForeignKey(
                        name: "FK_BatchTrainees_Batches_BatchId",
                        column: x => x.BatchId,
                        principalSchema: "users",
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchTrainees_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalSchema: "users",
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraineeSkills",
                schema: "users",
                columns: table => new
                {
                    TraineeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeSkills", x => new { x.TraineeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_TraineeSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "users",
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraineeSkills_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalSchema: "users",
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Batches",
                columns: new[] { "BatchId", "Description", "EndDate", "Name", "StartDate", "Status", "Timeline" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Introductory batch for C#", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Beginners", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", "Mon-Wed-Fri" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Deep dive into SQL optimization", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced SQL", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Planned", "Tue-Thu" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Trainees",
                columns: new[] { "TraineeId", "EducationalBackground", "Email", "ExperienceLevel", "GithubUsername", "LearningObjectives", "Name", "ProfileImage", "TraineeCV" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "BSc Computer Science", null, "Beginner", "ahmed-dev", "Learn backend development", null, "ahmed.png", "ahmed_cv.pdf" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "MSc Data Science", null, "Intermediate", "fatima-coder", "Advance SQL skills", null, "fatima.png", "fatima_cv.pdf" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "BatchTrainees",
                columns: new[] { "BatchId", "TraineeId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("44444444-4444-4444-4444-444444444444") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchTrainees_TraineeId",
                schema: "users",
                table: "BatchTrainees",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSkills_SkillId",
                schema: "users",
                table: "TraineeSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchTrainees",
                schema: "users");

            migrationBuilder.DropTable(
                name: "TraineeSkills",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Batches",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Trainees",
                schema: "users");
        }
    }
}
