using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reports");

            migrationBuilder.RenameTable(
                name: "ReasonCodes",
                newName: "ReasonCodes",
                newSchema: "reports");

            migrationBuilder.RenameTable(
                name: "Geolocations",
                newName: "Geolocations",
                newSchema: "reports");

            migrationBuilder.RenameTable(
                name: "FileStorages",
                newName: "FileStorages",
                newSchema: "reports");

            migrationBuilder.RenameTable(
                name: "BranchReports",
                newName: "BranchReports",
                newSchema: "reports");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branches",
                newSchema: "reports");

            migrationBuilder.RenameTable(
                name: "AttendanceRecord",
                newName: "AttendanceRecord",
                newSchema: "reports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ReasonCodes",
                schema: "reports",
                newName: "ReasonCodes");

            migrationBuilder.RenameTable(
                name: "Geolocations",
                schema: "reports",
                newName: "Geolocations");

            migrationBuilder.RenameTable(
                name: "FileStorages",
                schema: "reports",
                newName: "FileStorages");

            migrationBuilder.RenameTable(
                name: "BranchReports",
                schema: "reports",
                newName: "BranchReports");

            migrationBuilder.RenameTable(
                name: "Branches",
                schema: "reports",
                newName: "Branches");

            migrationBuilder.RenameTable(
                name: "AttendanceRecord",
                schema: "reports",
                newName: "AttendanceRecord");
        }
    }
}
