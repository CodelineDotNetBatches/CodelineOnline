using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class AddGeolocationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
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

            migrationBuilder.CreateTable(
                name: "AttendanceRecord",
                columns: table => new
                {
                    AttId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonCodeId = table.Column<int>(type: "int", nullable: true),
                    CapturedPhotoId = table.Column<int>(type: "int", nullable: false),
                    FaceMatchScore = table.Column<double>(type: "float", nullable: false),
                    LivenessScore = table.Column<double>(type: "float", nullable: false),
                    GeolocationId = table.Column<int>(type: "int", nullable: false),
                    ReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecord", x => x.AttId);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_FileStorages_CapturedPhotoId",
                        column: x => x.CapturedPhotoId,
                        principalTable: "FileStorages",
                        principalColumn: "FileStorageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_Geolocations_GeolocationId",
                        column: x => x.GeolocationId,
                        principalTable: "Geolocations",
                        principalColumn: "GeolocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_ReasonCodes_ReasonCodeId",
                        column: x => x.ReasonCodeId,
                        principalTable: "ReasonCodes",
                        principalColumn: "ReasonCodeId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_CapturedPhotoId",
                table: "AttendanceRecord",
                column: "CapturedPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_GeolocationId",
                table: "AttendanceRecord",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_ReasonCodeId",
                table: "AttendanceRecord",
                column: "ReasonCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRecord");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "BranchReports");

            migrationBuilder.DropTable(
                name: "FileStorages");

            migrationBuilder.DropTable(
                name: "Geolocations");

            migrationBuilder.DropTable(
                name: "ReasonCodes");
        }
    }
}
