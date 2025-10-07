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
                name: "branchs",
                schema: "users",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branchs", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "AdminProfiles",
                schema: "users",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProfiles", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_AdminProfiles_branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "users",
                        principalTable: "branchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "branchPNs",
                schema: "users",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branchPNs", x => new { x.BranchId, x.PhoneNumber });
                    table.ForeignKey(
                        name: "FK_branchPNs_branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "users",
                        principalTable: "branchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
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
                    Specialization = table.Column<int>(type: "int", maxLength: 80, nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructors_branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "users",
                        principalTable: "branchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "users",
                columns: table => new
                {
                    RoomNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.RoomNumber);
                    table.ForeignKey(
                        name: "FK_rooms_branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "users",
                        principalTable: "branchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                schema: "users",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Batches_branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "users",
                        principalTable: "branchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
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
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false),
                    avilabilityId = table.Column<int>(type: "int", nullable: false),
                    Avail_Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    day_of_week = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => new { x.InstructorId, x.time });
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
                    SkillName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    InstructorSkillId = table.Column<int>(type: "int", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSkills", x => new { x.InstructorId, x.SkillName });
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
                        onDelete: ReferentialAction.Restrict);
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
                    LearningObjectives = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Trainees_branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "users",
                        principalTable: "branchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraineeSkills",
                schema: "users",
                columns: table => new
                {
                    SkillName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    TraineeSkillId = table.Column<int>(type: "int", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeSkills", x => new { x.TraineeId, x.SkillName });
                    table.ForeignKey(
                        name: "FK_TraineeSkills_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalSchema: "users",
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

    //        migrationBuilder.InsertData(
    //            schema: "users",
    //            table: "AdminProfiles",
    //            columns: new[] { "AdminId", "BranchId" },
    //            values: new object[,]
    //            {
    //                { 1, 1 },
    //                { 2, 2 },
    //                { 3, 1 }
    //            });

    //        migrationBuilder.InsertData(
    //schema: "users",
    //table: "Branch",
    //columns: new[] { "BranchId", "City", "BranchName", "Country", "Email", "IsActive" },
    //values: new object[,]
    //{
    //    { 1, "Muscat", "Main Branch", "Oman", "muscat@center.com", true },
    //    { 2, "Salalah", "South Branch", "Oman", "salalah@center.com", true }
    //});


            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_BranchId",
                schema: "users",
                table: "AdminProfiles",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_Avail_Status",
                schema: "users",
                table: "Availabilities",
                column: "Avail_Status");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_avilabilityId",
                schema: "users",
                table: "Availabilities",
                column: "avilabilityId",
                unique: true);

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
                name: "IX_Batches_BranchId",
                schema: "users",
                table: "Batches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInstructor_InstructorsInstructorId",
                schema: "users",
                table: "BatchInstructor",
                column: "InstructorsInstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_BranchId",
                schema: "users",
                table: "Instructors",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Specialization",
                schema: "users",
                table: "Instructors",
                column: "Specialization");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSkills_InstructorSkillId",
                schema: "users",
                table: "InstructorSkills",
                column: "InstructorSkillId");

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
                name: "IX_rooms_BranchId",
                schema: "users",
                table: "rooms",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_BatchId",
                schema: "users",
                table: "Trainees",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_BranchId",
                schema: "users",
                table: "Trainees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSkills_TraineeSkillId",
                schema: "users",
                table: "TraineeSkills",
                column: "TraineeSkillId");
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
                name: "branchPNs",
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
                name: "rooms",
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

            migrationBuilder.DropTable(
                name: "branchs",
                schema: "users");
        }
    }
}
