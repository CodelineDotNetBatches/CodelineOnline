using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "users",
                table: "AdminProfiles",
                column: "AdminId",
                values: new object[]
                {
                    1,
                    2,
                    3
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "AdminProfiles",
                keyColumn: "AdminId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AdminProfiles",
                keyColumn: "AdminId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AdminProfiles",
                keyColumn: "AdminId",
                keyValue: 3);
        }
    }
}
