using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Marca_Categoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Inventarios");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaID_Categoria",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_Categoria",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_Marca",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarcaID_Marca",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID_Categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID_Categoria);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    ID_Marca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.ID_Marca);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_CategoriaID_Categoria",
                table: "Inventarios",
                column: "CategoriaID_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_MarcaID_Marca",
                table: "Inventarios",
                column: "MarcaID_Marca");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Categorias_CategoriaID_Categoria",
                table: "Inventarios",
                column: "CategoriaID_Categoria",
                principalTable: "Categorias",
                principalColumn: "ID_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Marcas_MarcaID_Marca",
                table: "Inventarios",
                column: "MarcaID_Marca",
                principalTable: "Marcas",
                principalColumn: "ID_Marca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Categorias_CategoriaID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Marcas_MarcaID_Marca",
                table: "Inventarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_CategoriaID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_MarcaID_Marca",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "CategoriaID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "ID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "ID_Marca",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "MarcaID_Marca",
                table: "Inventarios");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Inventarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Inventarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
