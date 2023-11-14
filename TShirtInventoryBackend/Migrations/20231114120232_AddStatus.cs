using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TshirtInventoryBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "TshirtOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TshirtOrders_StatusId",
                table: "TshirtOrders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TshirtOrders_Status_StatusId",
                table: "TshirtOrders",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TshirtOrders_Status_StatusId",
                table: "TshirtOrders");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_TshirtOrders_StatusId",
                table: "TshirtOrders");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TshirtOrders");
        }
    }
}
