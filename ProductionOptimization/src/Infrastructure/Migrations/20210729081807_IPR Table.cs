using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class IPRTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPRs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UseLiftTable = table.Column<bool>(type: "bit", nullable: false),
                    WaterFractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GasFractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservoirPressureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservoirTemperatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductivityIndexId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiftTableContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiftTablePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemAnalysisModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPRs_ParamEntries_GasFractionId",
                        column: x => x.GasFractionId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPRs_ParamEntries_ProductivityIndexId",
                        column: x => x.ProductivityIndexId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPRs_ParamEntries_ReservoirPressureId",
                        column: x => x.ReservoirPressureId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPRs_ParamEntries_ReservoirTemperatureId",
                        column: x => x.ReservoirTemperatureId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPRs_ParamEntries_WaterFractionId",
                        column: x => x.WaterFractionId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPRs_SystemAnalysisModels_SystemAnalysisModelId",
                        column: x => x.SystemAnalysisModelId,
                        principalTable: "SystemAnalysisModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPRs_GasFractionId",
                table: "IPRs",
                column: "GasFractionId");

            migrationBuilder.CreateIndex(
                name: "IX_IPRs_ProductivityIndexId",
                table: "IPRs",
                column: "ProductivityIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_IPRs_ReservoirPressureId",
                table: "IPRs",
                column: "ReservoirPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_IPRs_ReservoirTemperatureId",
                table: "IPRs",
                column: "ReservoirTemperatureId");

            migrationBuilder.CreateIndex(
                name: "IX_IPRs_SystemAnalysisModelId",
                table: "IPRs",
                column: "SystemAnalysisModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IPRs_WaterFractionId",
                table: "IPRs",
                column: "WaterFractionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPRs");
        }
    }
}
