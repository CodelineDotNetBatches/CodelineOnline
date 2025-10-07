using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoursesManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixProgramCategoriesCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramCategories",
                schema: "courses",
                table: "ProgramCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProgramCategories_ProgramsProgramId",
                schema: "courses",
                table: "ProgramCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramCategories",
                schema: "courses",
                table: "ProgramCategories",
                columns: new[] { "ProgramsProgramId", "CategoriesCategoryId" });

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

            migrationBuilder.InsertData(
                schema: "courses",
                table: "ProgramCategories",
                columns: new[] { "CategoriesCategoryId", "ProgramsProgramId" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("22222222-2222-2222-2222-222222222222") }
                });

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
                name: "IX_ProgramCategories_CategoriesCategoryId",
                schema: "courses",
                table: "ProgramCategories",
                column: "CategoriesCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramCategories",
                schema: "courses",
                table: "ProgramCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProgramCategories_CategoriesCategoryId",
                schema: "courses",
                table: "ProgramCategories");

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "ProgramCategories",
                keyColumns: new[] { "CategoriesCategoryId", "ProgramsProgramId" },
                keyValues: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                schema: "courses",
                table: "ProgramCategories",
                keyColumns: new[] { "CategoriesCategoryId", "ProgramsProgramId" },
                keyValues: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramCategories",
                schema: "courses",
                table: "ProgramCategories",
                columns: new[] { "CategoriesCategoryId", "ProgramsProgramId" });

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(7717));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(8721));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(9472));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(1171));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(1176));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 17, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(3683));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "EnrolledAt",
                value: new DateTime(2025, 8, 23, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(5009));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("cccc3333-cccc-cccc-cccc-cccccccccccc"),
                column: "EnrolledAt",
                value: new DateTime(2025, 9, 27, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: new Guid("dddd4444-dddd-dddd-dddd-dddddddddddd"),
                column: "EnrolledAt",
                value: new DateTime(2025, 10, 2, 12, 58, 35, 722, DateTimeKind.Utc).AddTicks(5422));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(5097));

            migrationBuilder.UpdateData(
                schema: "courses",
                table: "Programs",
                keyColumn: "ProgramId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 7, 12, 58, 35, 721, DateTimeKind.Utc).AddTicks(6767));

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCategories_ProgramsProgramId",
                schema: "courses",
                table: "ProgramCategories",
                column: "ProgramsProgramId");
        }
    }
}
