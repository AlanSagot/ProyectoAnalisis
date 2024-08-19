using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CambiosPlanillaEmpleadoIncapacidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Categorias_CategoriaID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Estados_EstadoID_Estado",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Marcas_MarcaID_Marca",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Planillas_EmpleadoId",
                table: "Planillas");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_CategoriaID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_EstadoID_Estado",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_MarcaID_Marca",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "CategoriaID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "EstadoID_Estado",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "MarcaID_Marca",
                table: "Inventarios");

            migrationBuilder.RenameColumn(
                name: "Salario",
                table: "Planillas",
                newName: "HorasTrabajadas");

            migrationBuilder.RenameColumn(
                name: "PlanillaId",
                table: "Empleados",
                newName: "Vacaciones");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Empleados",
                newName: "Sexo");

            migrationBuilder.AddColumn<double>(
                name: "DeduccionesEmbargo",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Feriado",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HorasExtra",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrecioPorHora",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RebajoCCSS",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RebajoINS",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RebajoIncapacidadCCSS",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RebajoIncapacidadINS",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioBruto",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioNeto",
                table: "Planillas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "Cedula",
                table: "Empleados",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Empleados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ID_Estado",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Incapacidades",
                columns: table => new
                {
                    IncapacidadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Porcentaje = table.Column<double>(type: "float", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incapacidades", x => x.IncapacidadId);
                    table.ForeignKey(
                        name: "FK_Incapacidades_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_EmpleadoId",
                table: "Planillas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_ID_Categoria",
                table: "Inventarios",
                column: "ID_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_ID_Estado",
                table: "Inventarios",
                column: "ID_Estado");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_ID_Marca",
                table: "Inventarios",
                column: "ID_Marca");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_ID_Estado",
                table: "Empleados",
                column: "ID_Estado");

            migrationBuilder.CreateIndex(
                name: "IX_Incapacidades_EmpleadoId",
                table: "Incapacidades",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Estados_ID_Estado",
                table: "Empleados",
                column: "ID_Estado",
                principalTable: "Estados",
                principalColumn: "ID_Estado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Categorias_ID_Categoria",
                table: "Inventarios",
                column: "ID_Categoria",
                principalTable: "Categorias",
                principalColumn: "ID_Categoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Estados_ID_Estado",
                table: "Inventarios",
                column: "ID_Estado",
                principalTable: "Estados",
                principalColumn: "ID_Estado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Marcas_ID_Marca",
                table: "Inventarios",
                column: "ID_Marca",
                principalTable: "Marcas",
                principalColumn: "ID_Marca",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Estados_ID_Estado",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Categorias_ID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Estados_ID_Estado",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Marcas_ID_Marca",
                table: "Inventarios");

            migrationBuilder.DropTable(
                name: "Incapacidades");

            migrationBuilder.DropIndex(
                name: "IX_Planillas_EmpleadoId",
                table: "Planillas");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_ID_Categoria",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_ID_Estado",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_ID_Marca",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_ID_Estado",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "DeduccionesEmbargo",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "Feriado",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "HorasExtra",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "PrecioPorHora",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "RebajoCCSS",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "RebajoINS",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "RebajoIncapacidadCCSS",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "RebajoIncapacidadINS",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "SalarioBruto",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "SalarioNeto",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "ID_Estado",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "HorasTrabajadas",
                table: "Planillas",
                newName: "Salario");

            migrationBuilder.RenameColumn(
                name: "Vacaciones",
                table: "Empleados",
                newName: "PlanillaId");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Empleados",
                newName: "Estado");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaID_Categoria",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoID_Estado",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaID_Marca",
                table: "Inventarios",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                table: "Empleados",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_EmpleadoId",
                table: "Planillas",
                column: "EmpleadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_CategoriaID_Categoria",
                table: "Inventarios",
                column: "CategoriaID_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_EstadoID_Estado",
                table: "Inventarios",
                column: "EstadoID_Estado");

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
                name: "FK_Inventarios_Estados_EstadoID_Estado",
                table: "Inventarios",
                column: "EstadoID_Estado",
                principalTable: "Estados",
                principalColumn: "ID_Estado");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Marcas_MarcaID_Marca",
                table: "Inventarios",
                column: "MarcaID_Marca",
                principalTable: "Marcas",
                principalColumn: "ID_Marca");
        }
    }
}
