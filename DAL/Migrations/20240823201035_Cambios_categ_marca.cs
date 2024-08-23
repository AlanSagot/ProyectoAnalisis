using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Cambios_categ_marca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Categorias_ID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Marcas_ID_Marca",
                table: "Inventarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marcas",
                table: "Marcas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "Marcas",
                newName: "Marca");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marca",
                table: "Marca",
                column: "ID_Marca");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "ID_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Categoria_ID_Categoria",
                table: "Inventarios",
                column: "ID_Categoria",
                principalTable: "Categoria",
                principalColumn: "ID_Categoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Marca_ID_Marca",
                table: "Inventarios",
                column: "ID_Marca",
                principalTable: "Marca",
                principalColumn: "ID_Marca",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Categoria_ID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Marca_ID_Marca",
                table: "Inventarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marca",
                table: "Marca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.RenameTable(
                name: "Marca",
                newName: "Marcas");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "Categorias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marcas",
                table: "Marcas",
                column: "ID_Marca");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "ID_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Categorias_ID_Categoria",
                table: "Inventarios",
                column: "ID_Categoria",
                principalTable: "Categorias",
                principalColumn: "ID_Categoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Marcas_ID_Marca",
                table: "Inventarios",
                column: "ID_Marca",
                principalTable: "Marcas",
                principalColumn: "ID_Marca",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
