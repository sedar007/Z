using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifyUserEntityBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellnessMetrics_Users_UserId",
                table: "WellnessMetrics");

            migrationBuilder.AddForeignKey(
                name: "FK_WellnessMetrics_Users_UserId",
                table: "WellnessMetrics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellnessMetrics_Users_UserId",
                table: "WellnessMetrics");

            migrationBuilder.AddForeignKey(
                name: "FK_WellnessMetrics_Users_UserId",
                table: "WellnessMetrics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
