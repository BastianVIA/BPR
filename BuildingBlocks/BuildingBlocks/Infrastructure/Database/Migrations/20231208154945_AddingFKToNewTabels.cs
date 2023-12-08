using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddingFKToNewTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "TestErrorModel");

            migrationBuilder.DropColumn(
                name: "ArticleName",
                table: "Actuators");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleNumber",
                table: "Actuators",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TestErrorModel_ErrorCode",
                table: "TestErrorModel",
                column: "ErrorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_ArticleNumber",
                table: "Actuators",
                column: "ArticleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Actuators_ArticleModel_ArticleNumber",
                table: "Actuators",
                column: "ArticleNumber",
                principalTable: "ArticleModel",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestErrorModel_TestErrorCodeModel_ErrorCode",
                table: "TestErrorModel",
                column: "ErrorCode",
                principalTable: "TestErrorCodeModel",
                principalColumn: "ErrorCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actuators_ArticleModel_ArticleNumber",
                table: "Actuators");

            migrationBuilder.DropForeignKey(
                name: "FK_TestErrorModel_TestErrorCodeModel_ErrorCode",
                table: "TestErrorModel");

            migrationBuilder.DropIndex(
                name: "IX_TestErrorModel_ErrorCode",
                table: "TestErrorModel");

            migrationBuilder.DropIndex(
                name: "IX_Actuators_ArticleNumber",
                table: "Actuators");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "TestErrorModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleNumber",
                table: "Actuators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ArticleName",
                table: "Actuators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
