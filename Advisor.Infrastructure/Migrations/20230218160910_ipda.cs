using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advisor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ipda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_investmentStrategies_InvestorInfos_InvestorInfoID",
                table: "investmentStrategies");

            migrationBuilder.DropForeignKey(
                name: "FK_investmentStrategies_Usersd_UserID",
                table: "investmentStrategies");

            migrationBuilder.DropIndex(
                name: "IX_investmentStrategies_UserID",
                table: "investmentStrategies");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "investmentStrategies");

            migrationBuilder.AlterColumn<int>(
                name: "InvestorInfoID",
                table: "investmentStrategies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_investmentStrategies_InvestorInfos_InvestorInfoID",
                table: "investmentStrategies",
                column: "InvestorInfoID",
                principalTable: "InvestorInfos",
                principalColumn: "InvestorInfoID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_investmentStrategies_InvestorInfos_InvestorInfoID",
                table: "investmentStrategies");

            migrationBuilder.AlterColumn<int>(
                name: "InvestorInfoID",
                table: "investmentStrategies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "investmentStrategies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_investmentStrategies_UserID",
                table: "investmentStrategies",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_investmentStrategies_InvestorInfos_InvestorInfoID",
                table: "investmentStrategies",
                column: "InvestorInfoID",
                principalTable: "InvestorInfos",
                principalColumn: "InvestorInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_investmentStrategies_Usersd_UserID",
                table: "investmentStrategies",
                column: "UserID",
                principalTable: "Usersd",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
