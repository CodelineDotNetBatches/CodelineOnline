using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursesManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "courses");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "courses",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                schema: "courses",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProgramDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Roadmap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "courses",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CourseLevel = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "courses",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramCategories",
                schema: "courses",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramsProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramCategories", x => new { x.CategoriesCategoryId, x.ProgramsProgramId });
                    table.ForeignKey(
                        name: "FK_ProgramCategories_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalSchema: "courses",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramCategories_Programs_ProgramsProgramId",
                        column: x => x.ProgramsProgramId,
                        principalSchema: "courses",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                schema: "courses",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    StatusChangeReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "courses",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "courses",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProgramCourses",
                schema: "courses",
                columns: table => new
                {
                    CoursesCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramsProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramCourses", x => new { x.CoursesCourseId, x.ProgramsProgramId });
                    table.ForeignKey(
                        name: "FK_ProgramCourses_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalSchema: "courses",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramCourses_Programs_ProgramsProgramId",
                        column: x => x.ProgramsProgramId,
                        principalSchema: "courses",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                schema: "courses",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                schema: "courses",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseName",
                schema: "courses",
                table: "Courses",
                column: "CourseName");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                schema: "courses",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ProgramId",
                schema: "courses",
                table: "Enrollments",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCategories_ProgramsProgramId",
                schema: "courses",
                table: "ProgramCategories",
                column: "ProgramsProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCourses_ProgramsProgramId",
                schema: "courses",
                table: "ProgramCourses",
                column: "ProgramsProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProgramName",
                schema: "courses",
                table: "Programs",
                column: "ProgramName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "ProgramCategories",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "ProgramCourses",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Programs",
                schema: "courses");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "courses");
        }
    }
}
