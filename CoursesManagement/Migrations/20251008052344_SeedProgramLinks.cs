using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoursesManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedProgramLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramCourses",
                schema: "courses",
                table: "ProgramCourses");

            migrationBuilder.DropIndex(
                name: "IX_ProgramCourses_ProgramsProgramId",
                schema: "courses",
                table: "ProgramCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramCourses",
                schema: "courses",
                table: "ProgramCourses",
                columns: new[] { "ProgramsProgramId", "CoursesCourseId" });

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(6965));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(7908));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(8600));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(9992));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(9997));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 18, 5, 23, 43, 509, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 24, 5, 23, 43, 509, DateTimeKind.Utc).AddTicks(3433));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("cccc3333-cccc-cccc-cccc-cccccccccccc"),
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 28, 5, 23, 43, 509, DateTimeKind.Utc).AddTicks(3809));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("dddd4444-dddd-dddd-dddd-dddddddddddd"),
                column: "EnrolledAt",
                value: new DateTime(2025, 10, 3, 5, 23, 43, 509, DateTimeKind.Utc).AddTicks(3814));

            migrationBuilder.InsertData(
                schema: "courses",
                table: "ProgramCourses",
                columns: new[] { "CoursesCourseId", "ProgramsProgramId" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(4793));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 5, 23, 43, 508, DateTimeKind.Utc).AddTicks(6238));

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCourses_CoursesCourseId",
                schema: "courses",
                table: "ProgramCourses",
                column: "CoursesCourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramCourses",
                schema: "courses",
                table: "ProgramCourses");

            migrationBuilder.DropIndex(
                name: "IX_ProgramCourses_CoursesCourseId",
                schema: "courses",
                table: "ProgramCourses");

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "ProgramCourses",
                keyColumns: new[] { "CoursesCourseId", "ProgramsProgramId" },
                keyValues: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "ProgramCourses",
                keyColumns: new[] { "CoursesCourseId", "ProgramsProgramId" },
                keyValues: new object[] { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "ProgramCourses",
                keyColumns: new[] { "CoursesCourseId", "ProgramsProgramId" },
                keyValues: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramCourses",
                schema: "courses",
                table: "ProgramCourses",
                columns: new[] { "CoursesCourseId", "ProgramsProgramId" });

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 996, DateTimeKind.Utc).AddTicks(7820));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 996, DateTimeKind.Utc).AddTicks(8766));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 996, DateTimeKind.Utc).AddTicks(9462));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 997, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 997, DateTimeKind.Utc).AddTicks(969));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 17, 14, 11, 9, 997, DateTimeKind.Utc).AddTicks(3218));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 23, 14, 11, 9, 997, DateTimeKind.Utc).AddTicks(4528));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("cccc3333-cccc-cccc-cccc-cccccccccccc"),
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 27, 14, 11, 9, 997, DateTimeKind.Utc).AddTicks(4876));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("dddd4444-dddd-dddd-dddd-dddddddddddd"),
                column: "EnrolledAt",
                value: new DateTime(2025, 10, 2, 14, 11, 9, 997, DateTimeKind.Utc).AddTicks(4881));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 996, DateTimeKind.Utc).AddTicks(5648));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 14, 11, 9, 996, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCourses_ProgramsProgramId",
                schema: "courses",
                table: "ProgramCourses",
                column: "ProgramsProgramId");
        }
    }
}
