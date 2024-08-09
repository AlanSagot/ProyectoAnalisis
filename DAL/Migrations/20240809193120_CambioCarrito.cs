using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CambioCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventarioProductoId",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoProducto",
                table: "Carritos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subtotal",
                table: "Carritos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_InventarioProductoId",
                table: "Inventarios",
                column: "InventarioProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Inventarios_InventarioProductoId",
                table: "Inventarios",
                column: "InventarioProductoId",
                principalTable: "Inventarios",
                principalColumn: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Inventarios_InventarioProductoId",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_InventarioProductoId",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "InventarioProductoId",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "FotoProducto",
                table: "Carritos");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Carritos");
        }
    }
}
