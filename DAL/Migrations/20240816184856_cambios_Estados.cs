using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class cambios_Estados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoID_Estado",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_Estado",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    ID_Estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.ID_Estado);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_EstadoID_Estado",
                table: "Inventarios",
                column: "EstadoID_Estado");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Estados_EstadoID_Estado",
                table: "Inventarios",
                column: "EstadoID_Estado",
                principalTable: "Estados",
                principalColumn: "ID_Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Estados_EstadoID_Estado",
                table: "Inventarios");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_EstadoID_Estado",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "EstadoID_Estado",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "ID_Estado",
                table: "Inventarios");
        }
    }
}
