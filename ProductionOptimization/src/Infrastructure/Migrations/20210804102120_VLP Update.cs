using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class VLPUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VLP_SystemAnalysisModelId",
                table: "VLP");

            migrationBuilder.CreateIndex(
                name: "IX_VLP_SystemAnalysisModelId",
                table: "VLP",
                column: "SystemAnalysisModelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VLP_SystemAnalysisModelId",
                table: "VLP");

            migrationBuilder.CreateIndex(
                name: "IX_VLP_SystemAnalysisModelId",
                table: "VLP",
                column: "SystemAnalysisModelId");
        }
    }
}
