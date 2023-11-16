using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TshirtInventoryBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCycleFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TshirtOrders_Tshirts_TshirtId",
                table: "TshirtOrders");

            migrationBuilder.DropIndex(
                name: "IX_TshirtOrders_TshirtId",
                table: "TshirtOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TshirtOrders_TshirtId",
                table: "TshirtOrders",
                column: "TshirtId");

            migrationBuilder.AddForeignKey(
                name: "FK_TshirtOrders_Tshirts_TshirtId",
                table: "TshirtOrders",
                column: "TshirtId",
                principalTable: "Tshirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
