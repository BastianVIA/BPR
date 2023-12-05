using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWoNoAndSerialNoFromTestError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "TestErrorModel");

            migrationBuilder.DropColumn(
                name: "WorkOrderNumber",
                table: "TestErrorModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "TestErrorModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkOrderNumber",
                table: "TestErrorModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
