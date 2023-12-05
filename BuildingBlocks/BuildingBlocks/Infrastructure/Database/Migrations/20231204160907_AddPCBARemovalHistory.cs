using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPCBARemovalHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActuatorPCBAHistoryModel",
                columns: table => new
                {
                    WorkOrderNumber = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    PCBAUid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RemovalTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActuatorPCBAHistoryModel", x => new { x.WorkOrderNumber, x.SerialNumber, x.PCBAUid, x.RemovalTime })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ActuatorPCBAHistoryModel_Actuators_WorkOrderNumber_SerialNumber",
                        columns: x => new { x.WorkOrderNumber, x.SerialNumber },
                        principalTable: "Actuators",
                        principalColumns: new[] { "WorkOrderNumber", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActuatorPCBAHistoryModel_PCBAs_PCBAUid",
                        column: x => x.PCBAUid,
                        principalTable: "PCBAs",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActuatorPCBAHistoryModel_PCBAUid",
                table: "ActuatorPCBAHistoryModel",
                column: "PCBAUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActuatorPCBAHistoryModel");
        }
    }
}
