using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Cambios_Envio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Inventarios_ProductoId",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_ProductoId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Detalle",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Impuesto",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Envios");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Envios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "Envios",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                table: "Envios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Canton",
                table: "Envios",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Distrito",
                table: "Envios",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventarioProductoId",
                table: "Envios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Envios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Envios",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Envios_InventarioProductoId",
                table: "Envios",
                column: "InventarioProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Inventarios_InventarioProductoId",
                table: "Envios",
                column: "InventarioProductoId",
                principalTable: "Inventarios",
                principalColumn: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Inventarios_InventarioProductoId",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_InventarioProductoId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Canton",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Distrito",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "InventarioProductoId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Envios");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Envios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "Envios",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                table: "Envios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detalle",
                table: "Envios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Impuesto",
                table: "Envios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Envios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Envios_ProductoId",
                table: "Envios",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Inventarios_ProductoId",
                table: "Envios",
                column: "ProductoId",
                principalTable: "Inventarios",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
