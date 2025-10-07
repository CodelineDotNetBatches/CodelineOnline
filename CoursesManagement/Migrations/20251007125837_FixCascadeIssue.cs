using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoursesManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("62a6b9b5-98f6-4830-948b-057a237c10e4"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("709e9037-0d5f-4dab-a947-63715a459b68"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("c4543298-a635-439e-adc4-65f6c56ffee0"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("0034175c-aa25-47e4-a13d-aa115a3b997e"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("d8bcb22a-bfb5-4314-a024-3fe180be0e0f"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("4ce6fe4b-998c-4ffc-b28e-558f0b3d9ed8"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("91afe1cd-ecef-4512-8f0c-90c37308d289"));

            migrationBuilder.CreateTable(
                name: "Certificates",
                schema: "courses",
                columns: table => new
                {
                    CertificateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificateUrl = table.Column<string>(type: "nvarchar(520)", maxLength: 520, nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_Certificates_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "courses",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificates_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalSchema: "courses",
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId");
                });

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName", "CreatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "C#, ASP.NET Core, SQL Server, APIs", "Backend Development", new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(7717) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Data visualization and analytics tools.", "Data Analytics", new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(8721) }
                });

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Programs",
                columns: new[] { "ProgramId", "CreatedAt", "ProgramDescription", "ProgramName", "Roadmap" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(5097), "Full stack software engineering program.", "Software Engineering", "Backend → Frontend → DevOps" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(6767), "Learn Python, statistics, and ML models.", "Data Science", "Python → Statistics → ML → Deployment" }
                });

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Courses",
                columns: new[] { "CourseId", "CategoryId", "CourseDescription", "CourseLevel", "CourseName", "CreatedAt", "Price" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("33333333-3333-3333-3333-333333333333"), "Learn how to build REST APIs using .NET Core.", 1, "ASP.NET Core Fundamentals", new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(9472), 0m },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("33333333-3333-3333-3333-333333333333"), "Database management with SQL Server and EF Core ORM.", 0, "SQL Server & EF Core", new DateTime(2025, 10, 7, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(1171), 0m },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("44444444-4444-4444-4444-444444444444"), "Introduction to ML algorithms and data preprocessing.", 1, "Machine Learning 101", new DateTime(2025, 10, 7, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(1176), 0m }
                });

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "CourseId", "EnrolledAt", "Grade", "ProgramId", "Status", "StatusChangeReason", "UserId" },
                values: new object[,]
                {
                    { new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 9, 17, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(3683), null, new Guid("11111111-1111-1111-1111-111111111111"), "Active", null, new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 8, 23, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(5009), 91.5m, new Guid("11111111-1111-1111-1111-111111111111"), "Completed", "Finished successfully", new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("cccc3333-cccc-cccc-cccc-cccccccccccc"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 9, 27, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(5418), null, new Guid("22222222-2222-2222-2222-222222222222"), "Active", null, new Guid("77777777-7777-4444-8888-111111111111") },
                    { new Guid("dddd4444-dddd-dddd-dddd-dddddddddddd"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 10, 2, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(5422), null, new Guid("11111111-1111-1111-1111-111111111111"), "Dropped", "Dropped due to absence", new Guid("99999999-9999-9999-9999-999999999999") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CertificateUrl",
                schema: "courses",
                table: "Certificates",
                column: "CertificateUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CourseId",
                schema: "courses",
                table: "Certificates",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_EnrollmentId",
                schema: "courses",
                table: "Certificates",
                column: "EnrollmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates",
                schema: "courses");

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("cccc3333-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("dddd4444-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName", "CreatedAt" },
                values: new object[,]
                {
                    { new Guid("4ce6fe4b-998c-4ffc-b28e-558f0b3d9ed8"), "Data visualization and analytics tools.", "Data Analytics", new DateTime(2025, 10, 7, 7, 13, 0, 97, DateTimeKind.Utc).AddTicks(9252) },
                    { new Guid("91afe1cd-ecef-4512-8f0c-90c37308d289"), "C#, ASP.NET Core, SQL Server, APIs", "Backend Development", new DateTime(2025, 10, 7, 7, 13, 0, 97, DateTimeKind.Utc).AddTicks(8893) }
                });

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Programs",
                columns: new[] { "ProgramId", "CreatedAt", "ProgramDescription", "ProgramName", "Roadmap" },
                values: new object[,]
                {
                    { new Guid("0034175c-aa25-47e4-a13d-aa115a3b997e"), new DateTime(2025, 10, 7, 7, 13, 0, 97, DateTimeKind.Utc).AddTicks(7750), "Full stack software engineering program.", "Software Engineering", "Backend → Frontend → DevOps" },
                    { new Guid("d8bcb22a-bfb5-4314-a024-3fe180be0e0f"), new DateTime(2025, 10, 7, 7, 13, 0, 97, DateTimeKind.Utc).AddTicks(7893), "Learn Python, statistics, and ML models.", "Data Science", "Python → Statistics → ML → Deployment" }
                });

            migrationBuilder.InsertData(
                schema: "courses",
                table: "Courses",
                columns: new[] { "CourseId", "CategoryId", "CourseDescription", "CourseLevel", "CourseName", "CreatedAt", "Price" },
                values: new object[,]
                {
                    { new Guid("62a6b9b5-98f6-4830-948b-057a237c10e4"), new Guid("91afe1cd-ecef-4512-8f0c-90c37308d289"), "Learn how to build REST APIs using .NET Core.", 1, "ASP.NET Core Fundamentals", new DateTime(2025, 10, 7, 7, 13, 0, 98, DateTimeKind.Utc).AddTicks(655), 0m },
                    { new Guid("709e9037-0d5f-4dab-a947-63715a459b68"), new Guid("4ce6fe4b-998c-4ffc-b28e-558f0b3d9ed8"), "Introduction to ML algorithms and data preprocessing.", 1, "Machine Learning 101", new DateTime(2025, 10, 7, 7, 13, 0, 98, DateTimeKind.Utc).AddTicks(788), 0m },
                    { new Guid("c4543298-a635-439e-adc4-65f6c56ffee0"), new Guid("91afe1cd-ecef-4512-8f0c-90c37308d289"), "Database management with SQL Server and EF Core ORM.", 0, "SQL Server & EF Core", new DateTime(2025, 10, 7, 7, 13, 0, 98, DateTimeKind.Utc).AddTicks(785), 0m }
                });
        }
    }
}
