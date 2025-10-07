using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class D : Migration
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
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "CourseReports",
                schema: "reports",
                columns: table => new
                {
                    CourseReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSessions = table.Column<int>(type: "int", nullable: false),
                    TotalStudents = table.Column<int>(type: "int", nullable: false),
                    AverageAttendanceRate = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReports", x => x.CourseReportId);
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
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorages", x => x.FileStorageId);
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

            migrationBuilder.CreateTable(
                name: "TrainerReports",
                schema: "reports",
                columns: table => new
                {
                    TrainerReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSessions = table.Column<int>(type: "int", nullable: false),
                    TotalStudents = table.Column<int>(type: "int", nullable: false),
                    AttendanceRate = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerReports", x => x.TrainerReportId);
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
                    table.ForeignKey(
                        name: "FK_BranchReports_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "reports",
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Geolocations",
                schema: "reports",
                columns: table => new
                {
                    GeolocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RediusMeters = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.GeolocationId);
                    table.ForeignKey(
                        name: "FK_Geolocations_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "reports",
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecord",
                schema: "reports",
                columns: table => new
                {
                    AttId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonCodeId = table.Column<int>(type: "int", nullable: true),
                    CapturedPhotoId = table.Column<int>(type: "int", nullable: true),
                    FaceMatchScore = table.Column<double>(type: "float", nullable: false),
                    LivenessScore = table.Column<double>(type: "float", nullable: false),
                    GeolocationId = table.Column<int>(type: "int", nullable: false),
                    ReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IdempotencyKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecord", x => x.AttId);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_FileStorages_CapturedPhotoId",
                        column: x => x.CapturedPhotoId,
                        principalSchema: "reports",
                        principalTable: "FileStorages",
                        principalColumn: "FileStorageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_Geolocations_GeolocationId",
                        column: x => x.GeolocationId,
                        principalSchema: "reports",
                        principalTable: "Geolocations",
                        principalColumn: "GeolocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_ReasonCodes_ReasonCodeId",
                        column: x => x.ReasonCodeId,
                        principalSchema: "reports",
                        principalTable: "ReasonCodes",
                        principalColumn: "ReasonCodeId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "GeoRadiusAudits",
                schema: "reports",
                columns: table => new
                {
                    GeoRadiusAuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeolocationId = table.Column<int>(type: "int", nullable: false),
                    OldRadius = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewRadius = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoRadiusAudits", x => x.GeoRadiusAuditId);
                    table.ForeignKey(
                        name: "FK_GeoRadiusAudits_Geolocations_GeolocationId",
                        column: x => x.GeolocationId,
                        principalSchema: "reports",
                        principalTable: "Geolocations",
                        principalColumn: "GeolocationId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_CapturedPhotoId",
                schema: "reports",
                table: "AttendanceRecord",
                column: "CapturedPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_GeolocationId",
                schema: "reports",
                table: "AttendanceRecord",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_ReasonCodeId",
                schema: "reports",
                table: "AttendanceRecord",
                column: "ReasonCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchReports_BranchId",
                schema: "reports",
                table: "BranchReports",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReports_CourseId",
                schema: "reports",
                table: "CourseReports",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_BranchId",
                schema: "reports",
                table: "Geolocations",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_GeoRadiusAudits_GeolocationId",
                schema: "reports",
                table: "GeoRadiusAudits",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReports_TrainerId_CourseId",
                schema: "reports",
                table: "TrainerReports",
                columns: new[] { "TrainerId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRecord",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "BranchReports",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "CourseReports",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "GeoRadiusAudits",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "TrainerReports",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "FileStorages",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReasonCodes",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "Geolocations",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "reports");
        }
    }
}
