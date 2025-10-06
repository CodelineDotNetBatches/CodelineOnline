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
                name: "AdminProfiles",
                schema: "users",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProfiles", x => x.AdminId);
                });

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
                name: "Batches",
                schema: "users",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    BatchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BatchStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BatchEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BatchTimeline = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    BatchDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    admin_ProfileAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_Batches_AdminProfiles_admin_ProfileAdminId",
                        column: x => x.admin_ProfileAdminId,
                        principalSchema: "users",
                        principalTable: "AdminProfiles",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsibilities",
                schema: "users",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    ResponsibilityTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResponsibilityDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminProfileAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibilities", x => new { x.AdminId, x.ResponsibilityTitle });
                    table.ForeignKey(
                        name: "FK_Responsibilities_AdminProfiles_AdminProfileAdminId",
                        column: x => x.AdminProfileAdminId,
                        principalSchema: "users",
                        principalTable: "AdminProfiles",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "InstructorSkills",
                schema: "users",
                columns: table => new
                {
                    InstructorSkillId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSkills", x => new { x.InstructorId, x.InstructorSkillId });
                    table.ForeignKey(
                        name: "FK_InstructorSkills_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalSchema: "users",
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchInstructor",
                schema: "users",
                columns: table => new
                {
                    BatchesBatchId = table.Column<int>(type: "int", nullable: false),
                    InstructorsInstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchInstructor", x => new { x.BatchesBatchId, x.InstructorsInstructorId });
                    table.ForeignKey(
                        name: "FK_BatchInstructor_Batches_BatchesBatchId",
                        column: x => x.BatchesBatchId,
                        principalSchema: "users",
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchInstructor_Instructors_InstructorsInstructorId",
                        column: x => x.InstructorsInstructorId,
                        principalSchema: "users",
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                schema: "users",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    GithubUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Years_of_Experience = table.Column<int>(type: "int", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TraineeCV = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Experience_Level = table.Column<int>(type: "int", nullable: false),
                    Learning_Style = table.Column<int>(type: "int", nullable: false),
                    Study_Focus = table.Column<int>(type: "int", nullable: false),
                    EducationalBackground = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LearningObjectives = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                    table.ForeignKey(
                        name: "FK_Trainees_Batches_BatchId",
                        column: x => x.BatchId,
                        principalSchema: "users",
                        principalTable: "Batches",
                        principalColumn: "BatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorTrainee",
                schema: "users",
                columns: table => new
                {
                    InstructorsInstructorId = table.Column<int>(type: "int", nullable: false),
                    TraineesTraineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorTrainee", x => new { x.InstructorsInstructorId, x.TraineesTraineeId });
                    table.ForeignKey(
                        name: "FK_InstructorTrainee_Instructors_InstructorsInstructorId",
                        column: x => x.InstructorsInstructorId,
                        principalSchema: "users",
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorTrainee_Trainees_TraineesTraineeId",
                        column: x => x.TraineesTraineeId,
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
                    TraineeSkillId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeSkills", x => new { x.TraineeId, x.TraineeSkillId });
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
                name: "IX_Batches_admin_ProfileAdminId",
                schema: "users",
                table: "Batches",
                column: "admin_ProfileAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_BatchName",
                schema: "users",
                table: "Batches",
                column: "BatchName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchInstructor_InstructorsInstructorId",
                schema: "users",
                table: "BatchInstructor",
                column: "InstructorsInstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Specialization",
                schema: "users",
                table: "Instructors",
                column: "Specialization");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSkills_SkillName",
                schema: "users",
                table: "InstructorSkills",
                column: "SkillName");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorTrainee_TraineesTraineeId",
                schema: "users",
                table: "InstructorTrainee",
                column: "TraineesTraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_AdminProfileAdminId",
                schema: "users",
                table: "Responsibilities",
                column: "AdminProfileAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_ResponsibilityTitle",
                schema: "users",
                table: "Responsibilities",
                column: "ResponsibilityTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_BatchId",
                schema: "users",
                table: "Trainees",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSkills_SkillName",
                schema: "users",
                table: "TraineeSkills",
                column: "SkillName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Availabilities",
                schema: "users");

            migrationBuilder.DropTable(
                name: "BatchInstructor",
                schema: "users");

            migrationBuilder.DropTable(
                name: "InstructorSkills",
                schema: "users");

            migrationBuilder.DropTable(
                name: "InstructorTrainee",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Responsibilities",
                schema: "users");

            migrationBuilder.DropTable(
                name: "TraineeSkills",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Instructors",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Trainees",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Batches",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AdminProfiles",
                schema: "users");
        }
    }
}
