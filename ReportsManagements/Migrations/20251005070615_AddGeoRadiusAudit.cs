using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class AddGeoRadiusAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoRadiusAudits",
                schema: "reports");
        }
    }
}
