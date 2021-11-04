using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class VLPProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPRs_ParamEntries_GasFractionId",
                table: "IPRs");

            migrationBuilder.DropForeignKey(
                name: "FK_IPRs_ParamEntries_ProductivityIndexId",
                table: "IPRs");

            migrationBuilder.DropForeignKey(
                name: "FK_IPRs_ParamEntries_ReservoirPressureId",
                table: "IPRs");

            migrationBuilder.DropForeignKey(
                name: "FK_IPRs_ParamEntries_ReservoirTemperatureId",
                table: "IPRs");

            migrationBuilder.DropForeignKey(
                name: "FK_IPRs_ParamEntries_WaterFractionId",
                table: "IPRs");

            migrationBuilder.DropForeignKey(
                name: "FK_IPRs_SystemAnalysisModels_SystemAnalysisModelId",
                table: "IPRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IPRs",
                table: "IPRs");

            migrationBuilder.RenameTable(
                name: "IPRs",
                newName: "IPR");

            migrationBuilder.RenameIndex(
                name: "IX_IPRs_WaterFractionId",
                table: "IPR",
                newName: "IX_IPR_WaterFractionId");

            migrationBuilder.RenameIndex(
                name: "IX_IPRs_SystemAnalysisModelId",
                table: "IPR",
                newName: "IX_IPR_SystemAnalysisModelId");

            migrationBuilder.RenameIndex(
                name: "IX_IPRs_ReservoirTemperatureId",
                table: "IPR",
                newName: "IX_IPR_ReservoirTemperatureId");

            migrationBuilder.RenameIndex(
                name: "IX_IPRs_ReservoirPressureId",
                table: "IPR",
                newName: "IX_IPR_ReservoirPressureId");

            migrationBuilder.RenameIndex(
                name: "IX_IPRs_ProductivityIndexId",
                table: "IPR",
                newName: "IX_IPR_ProductivityIndexId");

            migrationBuilder.RenameIndex(
                name: "IX_IPRs_GasFractionId",
                table: "IPR",
                newName: "IX_IPR_GasFractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IPR",
                table: "IPR",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VLP",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UseLiftTable = table.Column<bool>(type: "bit", nullable: false),
                    WaterFractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GasFractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    THPId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GasLiftFractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiftTableContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiftTablePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemAnalysisModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VLP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VLP_ParamEntries_GasFractionId",
                        column: x => x.GasFractionId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VLP_ParamEntries_GasLiftFractionId",
                        column: x => x.GasLiftFractionId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VLP_ParamEntries_THPId",
                        column: x => x.THPId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VLP_ParamEntries_WaterFractionId",
                        column: x => x.WaterFractionId,
                        principalTable: "ParamEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VLP_SystemAnalysisModels_SystemAnalysisModelId",
                        column: x => x.SystemAnalysisModelId,
                        principalTable: "SystemAnalysisModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VLP_GasFractionId",
                table: "VLP",
                column: "GasFractionId");

            migrationBuilder.CreateIndex(
                name: "IX_VLP_GasLiftFractionId",
                table: "VLP",
                column: "GasLiftFractionId");

            migrationBuilder.CreateIndex(
                name: "IX_VLP_SystemAnalysisModelId",
                table: "VLP",
                column: "SystemAnalysisModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VLP_THPId",
                table: "VLP",
                column: "THPId");

            migrationBuilder.CreateIndex(
                name: "IX_VLP_WaterFractionId",
                table: "VLP",
                column: "WaterFractionId");

            migrationBuilder.AddForeignKey(
                name: "FK_IPR_ParamEntries_GasFractionId",
                table: "IPR",
                column: "GasFractionId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPR_ParamEntries_ProductivityIndexId",
                table: "IPR",
                column: "ProductivityIndexId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPR_ParamEntries_ReservoirPressureId",
                table: "IPR",
                column: "ReservoirPressureId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPR_ParamEntries_ReservoirTemperatureId",
                table: "IPR",
                column: "ReservoirTemperatureId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPR_ParamEntries_WaterFractionId",
                table: "IPR",
                column: "WaterFractionId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPR_SystemAnalysisModels_SystemAnalysisModelId",
                table: "IPR",
                column: "SystemAnalysisModelId",
                principalTable: "SystemAnalysisModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPR_ParamEntries_GasFractionId",
                table: "IPR");

            migrationBuilder.DropForeignKey(
                name: "FK_IPR_ParamEntries_ProductivityIndexId",
                table: "IPR");

            migrationBuilder.DropForeignKey(
                name: "FK_IPR_ParamEntries_ReservoirPressureId",
                table: "IPR");

            migrationBuilder.DropForeignKey(
                name: "FK_IPR_ParamEntries_ReservoirTemperatureId",
                table: "IPR");

            migrationBuilder.DropForeignKey(
                name: "FK_IPR_ParamEntries_WaterFractionId",
                table: "IPR");

            migrationBuilder.DropForeignKey(
                name: "FK_IPR_SystemAnalysisModels_SystemAnalysisModelId",
                table: "IPR");

            migrationBuilder.DropTable(
                name: "VLP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IPR",
                table: "IPR");

            migrationBuilder.RenameTable(
                name: "IPR",
                newName: "IPRs");

            migrationBuilder.RenameIndex(
                name: "IX_IPR_WaterFractionId",
                table: "IPRs",
                newName: "IX_IPRs_WaterFractionId");

            migrationBuilder.RenameIndex(
                name: "IX_IPR_SystemAnalysisModelId",
                table: "IPRs",
                newName: "IX_IPRs_SystemAnalysisModelId");

            migrationBuilder.RenameIndex(
                name: "IX_IPR_ReservoirTemperatureId",
                table: "IPRs",
                newName: "IX_IPRs_ReservoirTemperatureId");

            migrationBuilder.RenameIndex(
                name: "IX_IPR_ReservoirPressureId",
                table: "IPRs",
                newName: "IX_IPRs_ReservoirPressureId");

            migrationBuilder.RenameIndex(
                name: "IX_IPR_ProductivityIndexId",
                table: "IPRs",
                newName: "IX_IPRs_ProductivityIndexId");

            migrationBuilder.RenameIndex(
                name: "IX_IPR_GasFractionId",
                table: "IPRs",
                newName: "IX_IPRs_GasFractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IPRs",
                table: "IPRs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IPRs_ParamEntries_GasFractionId",
                table: "IPRs",
                column: "GasFractionId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPRs_ParamEntries_ProductivityIndexId",
                table: "IPRs",
                column: "ProductivityIndexId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPRs_ParamEntries_ReservoirPressureId",
                table: "IPRs",
                column: "ReservoirPressureId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPRs_ParamEntries_ReservoirTemperatureId",
                table: "IPRs",
                column: "ReservoirTemperatureId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPRs_ParamEntries_WaterFractionId",
                table: "IPRs",
                column: "WaterFractionId",
                principalTable: "ParamEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IPRs_SystemAnalysisModels_SystemAnalysisModelId",
                table: "IPRs",
                column: "SystemAnalysisModelId",
                principalTable: "SystemAnalysisModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
