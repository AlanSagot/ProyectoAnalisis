using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using LeahMakeUp.Models;

namespace LeahMakeUp.Controllers
{
    public class PlanillasController : Controller
    {
        private readonly LeahDBContext _context;

        public PlanillasController(LeahDBContext context)
        {
            _context = context;
        }

        // GET: Planillas
        public async Task<IActionResult> Index()
        {
            var leahDBContext = _context.Planillas.Include(p => p.Puesto).Include(p => p.Sucursal).Include(p => p.Empleado);
            return View(await leahDBContext.ToListAsync());
        }

        // GET: Planillas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillas = await _context.Planillas
                .Include(p => p.Puesto)
                .Include(p => p.Sucursal)
                .Include(p => p.Empleado)
                .FirstOrDefaultAsync(m => m.PlanillaId == id);
            if (planillas == null)
            {
                return NotFound();
            }

            return View(planillas);
        }

        /// GET: Planillas/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Cedula");
            return View();
        }

        // POST: Planillas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanillasCreateViewModel planillas)
        {
            if (ModelState.IsValid)
            {
                // Obtén el empleado para poder acceder a los detalles necesarios
                var empleado = await _context.Empleados
                                             .Include(e => e.Puesto)
                                             .Include(e => e.Sucursal)
                                             .FirstOrDefaultAsync(e => e.EmpleadoId == planillas.EmpleadoId);

                if (empleado == null)
                {
                    return NotFound();
                }

                // Calcula el salario bruto
                double salarioBruto = (planillas.HorasTrabajadas + planillas.HorasExtra) * planillas.PrecioPorHora;

                // Calcula las deducciones
                double deduccionCCSS = Math.Round(salarioBruto * 0.0917, 2);
                double deduccionINS = Math.Round(salarioBruto * 0.01, 2);
                double otrasDeducciones = 0;

                // Suma todas las deducciones
                double totalDeducciones = deduccionCCSS + deduccionINS + otrasDeducciones;

                // Calcula el salario neto
                double salarioNeto = salarioBruto - totalDeducciones;

                // Crear la nueva planilla
                Planillas newPlanilla = new Planillas
                {
                    EmpleadoId = empleado.EmpleadoId,
                    PuestoId = empleado.PuestoId,
                    SucursalId = empleado.SucursalId,
                    PrecioPorHora = 1800,
                    HorasTrabajadas = planillas.HorasTrabajadas,
                    HorasExtra = planillas.HorasExtra,
                    SalarioBruto = salarioBruto,
                    RebajoCCSS = deduccionCCSS,
                    RebajoINS = deduccionINS,
                    Feriado = 1800,
                    SalarioNeto = salarioNeto
                };

                // Guardar la nueva planilla en la base de datos
                _context.Planillas.Add(newPlanilla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, volver a mostrar la vista con los campos seleccionados
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Cedula");
            return View(planillas);
        }

        //GET: Planillas/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla == null)
            {
                return NotFound();
            }

            // Obtener la información del empleado relacionado
            var empleado = await _context.Empleados.FindAsync(planilla.EmpleadoId);
            if (empleado == null)
            {
                return NotFound();
            }

            // Crear el ViewModel para la vista de edición
            var viewModel = new PlanillasCreateViewModel
            {
                PlanillaId = planilla.PlanillaId,
                EmpleadoId = empleado.EmpleadoId, 
                Cedula = empleado.Cedula,
                Nombre = empleado.Nombre,
                PrimerApellido = empleado.PrimerApellido,
                SegundoApellido = empleado.SegundoApellido,
                NombrePuesto = empleado.NombrePuesto,
                Direccion = empleado.Direccion,
                HorasTrabajadas = planilla.HorasTrabajadas,
                HorasExtra = planilla.HorasExtra,
                PrecioPorHora = planilla.PrecioPorHora
            };

            return View(viewModel);
        }

        //POST: Planillas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanillasCreateViewModel viewModel)
        {
            if (id != viewModel.PlanillaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var planilla = await _context.Planillas.FindAsync(id);
                    if (planilla == null)
                    {
                        return NotFound();
                    }

                    planilla.HorasTrabajadas = viewModel.HorasTrabajadas;
                    planilla.HorasExtra = viewModel.HorasExtra;
                    planilla.PrecioPorHora = viewModel.PrecioPorHora;

                    // Calcula los nuevos montos
                    double salarioBruto = (viewModel.HorasTrabajadas + viewModel.HorasExtra) * viewModel.PrecioPorHora;
                    double deduccionCCSS = Math.Round(salarioBruto * 0.0917, 2);
                    double deduccionINS = Math.Round(salarioBruto * 0.01, 2);
                    double salarioNeto = salarioBruto - (deduccionCCSS + deduccionINS);

                    // Actualiza el planilla con los nuevos valores
                    planilla.SalarioBruto = salarioBruto;
                    planilla.RebajoCCSS = deduccionCCSS;
                    planilla.RebajoINS = deduccionINS;
                    planilla.SalarioNeto = salarioNeto;

                    _context.Update(planilla);
                    await _context.SaveChangesAsync();

                    // Redirige a la vista Index
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillasExists(viewModel.PlanillaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Si el modelo no es válido, vuelve a la vista Edit
            return View(viewModel);
        }

        // GET: Planillas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var planilla = await _context.Planillas
                .FirstOrDefaultAsync(m => m.PlanillaId == id);

            if (planilla == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(planilla.EmpleadoId);
            if (empleado == null)
            {
                return NotFound();
            }

            var viewModel = new PlanillasCreateViewModel
            {
                PlanillaId = planilla.PlanillaId,
                EmpleadoId = empleado.EmpleadoId,
                Cedula = empleado.Cedula,
                Nombre = empleado.Nombre,
                PrimerApellido = empleado.PrimerApellido,
                SegundoApellido = empleado.SegundoApellido,
                NombrePuesto = empleado.NombrePuesto,
                Direccion = empleado.Direccion,
                HorasTrabajadas = planilla.HorasTrabajadas,
                HorasExtra = planilla.HorasExtra,
                PrecioPorHora = planilla.PrecioPorHora
            };

            return View(viewModel);
        }

        // POST: Planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla == null)
            {
                return NotFound();
            }

            _context.Planillas.Remove(planilla);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirige al índice después de eliminar
        }



        private bool PlanillasExists(int id)
        {
            return _context.Planillas.Any(e => e.PlanillaId == id);
        }

        [HttpGet("api/empleados/obtenerDatosPorEmpleadoId/{empleadoId}")]
        public async Task<IActionResult> ObtenerDatosPorEmpleadoId(int empleadoId)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Sucursal)
                .FirstOrDefaultAsync(e => e.EmpleadoId == empleadoId);

            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                cedula = empleado.Cedula,
                nombre = empleado.Nombre,
                primerApellido = empleado.PrimerApellido,
                segundoApellido = empleado.SegundoApellido,
                nombrePuesto = empleado.Puesto.NombrePuesto,
                direccion = empleado.Sucursal.Direccion
            });
        }

    }
}
