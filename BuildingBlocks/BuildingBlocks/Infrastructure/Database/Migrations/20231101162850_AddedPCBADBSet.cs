using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedPCBADBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actuators_PCBAModel_PCBAUid",
                table: "Actuators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PCBAModel",
                table: "PCBAModel");

            migrationBuilder.RenameTable(
                name: "PCBAModel",
                newName: "PCBAs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PCBAs",
                table: "PCBAs",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Actuators_PCBAs_PCBAUid",
                table: "Actuators",
                column: "PCBAUid",
                principalTable: "PCBAs",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actuators_PCBAs_PCBAUid",
                table: "Actuators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PCBAs",
                table: "PCBAs");

            migrationBuilder.RenameTable(
                name: "PCBAs",
                newName: "PCBAModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PCBAModel",
                table: "PCBAModel",
                column: "Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_Actuators_PCBAModel_PCBAUid",
                table: "Actuators",
                column: "PCBAUid",
                principalTable: "PCBAModel",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
