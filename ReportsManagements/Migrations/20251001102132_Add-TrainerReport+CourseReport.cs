using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerReportCourseReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseReports",
                schema: "reports",
                columns: table => new
                {
                    CourseReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSessions = table.Column<int>(type: "int", nullable: false),
                    TotalStudents = table.Column<int>(type: "int", nullable: false),
                    AverageAttendanceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReports", x => x.CourseReportId);
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
                    AttendanceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerReports", x => x.TrainerReportId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseReports",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "TrainerReports",
                schema: "reports");
        }
    }
}
