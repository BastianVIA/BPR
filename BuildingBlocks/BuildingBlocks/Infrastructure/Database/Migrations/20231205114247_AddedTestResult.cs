using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestResultModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderNumber = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    Tester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bay = table.Column<int>(type: "int", nullable: false),
                    MinServoPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxServoPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinBuslinkPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxBuslinkPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServoStroke = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResultModel");
        }
    }
}
