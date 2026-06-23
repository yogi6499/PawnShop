using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawnShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relation_restriction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoldItems_Loans_LoanId",
                table: "GoldItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Loans_LoanId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_GoldItems_Loans_LoanId",
                table: "GoldItems",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Loans_LoanId",
                table: "Payments",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoldItems_Loans_LoanId",
                table: "GoldItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Loans_LoanId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_GoldItems_Loans_LoanId",
                table: "GoldItems",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Loans_LoanId",
                table: "Payments",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
