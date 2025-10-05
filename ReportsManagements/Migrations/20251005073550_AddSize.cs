using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class AddSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AttendanceRate",
                schema: "reports",
                table: "TrainerReports",
                type: "decimal(5,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                schema: "reports",
                table: "FileStorages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageAttendanceRate",
                schema: "reports",
                table: "CourseReports",
                type: "decimal(5,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerReports_TrainerId_CourseId",
                schema: "reports",
                table: "TrainerReports",
                columns: new[] { "TrainerId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseReports_CourseId",
                schema: "reports",
                table: "CourseReports",
                column: "CourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainerReports_TrainerId_CourseId",
                schema: "reports",
                table: "TrainerReports");

            migrationBuilder.DropIndex(
                name: "IX_CourseReports_CourseId",
                schema: "reports",
                table: "CourseReports");

            migrationBuilder.DropColumn(
                name: "FileSize",
                schema: "reports",
                table: "FileStorages");

            migrationBuilder.AlterColumn<decimal>(
                name: "AttendanceRate",
                schema: "reports",
                table: "TrainerReports",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageAttendanceRate",
                schema: "reports",
                table: "CourseReports",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,3)");
        }
    }
}
