using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CambiosPlanillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Planillas_PlanillasPlanillaId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_PlanillasPlanillaId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "PlanillasPlanillaId",
                table: "Empleados");

            migrationBuilder.AddColumn<int>(
                name: "Empleado",
                table: "Planillas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Planillas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_EmpleadoId",
                table: "Planillas",
                column: "EmpleadoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Planillas_Empleados_EmpleadoId",
                table: "Planillas",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planillas_Empleados_EmpleadoId",
                table: "Planillas");

            migrationBuilder.DropIndex(
                name: "IX_Planillas_EmpleadoId",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "Empleado",
                table: "Planillas");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Planillas");

            migrationBuilder.AddColumn<int>(
                name: "PlanillasPlanillaId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_PlanillasPlanillaId",
                table: "Empleados",
                column: "PlanillasPlanillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Planillas_PlanillasPlanillaId",
                table: "Empleados",
                column: "PlanillasPlanillaId",
                principalTable: "Planillas",
                principalColumn: "PlanillaId");
        }
    }
}
