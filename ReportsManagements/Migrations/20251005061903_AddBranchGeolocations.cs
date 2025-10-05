using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsManagements.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchGeolocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_BranchId",
                schema: "reports",
                table: "Geolocations",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Geolocations_Branches_BranchId",
                schema: "reports",
                table: "Geolocations",
                column: "BranchId",
                principalSchema: "reports",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Geolocations_Branches_BranchId",
                schema: "reports",
                table: "Geolocations");

            migrationBuilder.DropIndex(
                name: "IX_Geolocations_BranchId",
                schema: "reports",
                table: "Geolocations");
        }
    }
}
