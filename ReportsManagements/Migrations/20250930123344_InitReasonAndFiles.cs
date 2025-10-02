using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class InitReasonAndFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reports");

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchReports",
                schema: "reports",
                columns: table => new
                {
                    BranchReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSessions = table.Column<int>(type: "int", nullable: false),
                    TotalStudents = table.Column<int>(type: "int", nullable: false),
                    AttendanceRate = table.Column<int>(type: "int", nullable: false),
                    TotalInstructors = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchReports", x => x.BranchReportId);
                });

            migrationBuilder.CreateTable(
                name: "FileStorages",
                schema: "reports",
                columns: table => new
                {
                    FileStorageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorages", x => x.FileStorageId);
                });

            migrationBuilder.CreateTable(
                name: "Geolocations",
                schema: "reports",
                columns: table => new
                {
                    GeolocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RediusMeters = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.GeolocationId);
                });

            migrationBuilder.CreateTable(
                name: "ReasonCodes",
                schema: "reports",
                columns: table => new
                {
                    ReasonCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonCodes", x => x.ReasonCodeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "BranchReports",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "FileStorages",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "Geolocations",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReasonCodes",
                schema: "reports");
        }
    }
}
