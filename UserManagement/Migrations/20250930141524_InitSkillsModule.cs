using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitSkillsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "users",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    MonthsOfExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                schema: "users",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                });

            migrationBuilder.CreateTable(
                name: "TraineeSkills",
                schema: "users",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraineeSkills", x => new { x.TraineeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_TraineeSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "users",
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraineeSkills_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalSchema: "users",
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraineeSkills_SkillId",
                schema: "users",
                table: "TraineeSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraineeSkills",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Trainees",
                schema: "users");
        }
    }
}
