using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class AddGeoRadiusAuditFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GeoRadiusAudits_GeolocationId",
                schema: "reports",
                table: "GeoRadiusAudits",
                column: "GeolocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoRadiusAudits_Geolocations_GeolocationId",
                schema: "reports",
                table: "GeoRadiusAudits",
                column: "GeolocationId",
                principalSchema: "reports",
                principalTable: "Geolocations",
                principalColumn: "GeolocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoRadiusAudits_Geolocations_GeolocationId",
                schema: "reports",
                table: "GeoRadiusAudits");

            migrationBuilder.DropIndex(
                name: "IX_GeoRadiusAudits_GeolocationId",
                schema: "reports",
                table: "GeoRadiusAudits");
        }
    }
}
