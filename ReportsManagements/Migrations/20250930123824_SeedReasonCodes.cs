using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class SeedReasonCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "reports",
                table: "ReasonCodes",
                columns: new[] { "ReasonCodeId", "Category", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Attendance", "LATE", true, "Late" },
                    { 2, "Health", "SICK", true, "Sick" },
                    { 3, "System", "TECH", true, "Technical Issue" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "reports",
                table: "ReasonCodes",
                keyColumn: "ReasonCodeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "reports",
                table: "ReasonCodes",
                keyColumn: "ReasonCodeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "reports",
                table: "ReasonCodes",
                keyColumn: "ReasonCodeId",
                keyValue: 3);
        }
    }
}
