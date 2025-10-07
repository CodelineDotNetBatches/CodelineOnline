using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoursesManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
