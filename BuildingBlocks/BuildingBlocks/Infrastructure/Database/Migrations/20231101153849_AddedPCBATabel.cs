using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedPCBATabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PCBAUid",
                table: "Actuators",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PCBAModel",
                columns: table => new
                {
                    Uid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManufacturerNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCBAModel", x => x.Uid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_PCBAUid",
                table: "Actuators",
                column: "PCBAUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Actuators_PCBAModel_PCBAUid",
                table: "Actuators",
                column: "PCBAUid",
                principalTable: "PCBAModel",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actuators_PCBAModel_PCBAUid",
                table: "Actuators");

            migrationBuilder.DropTable(
                name: "PCBAModel");

            migrationBuilder.DropIndex(
                name: "IX_Actuators_PCBAUid",
                table: "Actuators");

            migrationBuilder.AlterColumn<int>(
                name: "PCBAUid",
                table: "Actuators",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
