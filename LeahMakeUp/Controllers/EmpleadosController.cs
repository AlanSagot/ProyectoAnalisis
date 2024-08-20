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
    public class EmpleadosController : Controller
    {
        private readonly LeahDBContext _context;

        public EmpleadosController(LeahDBContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var leahDBContext = _context.Empleados.Include(e => e.Puesto).Include(e => e.Sucursal).Include(e => e.Estado);
            return View(await leahDBContext.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Sucursal)
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto");
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion");
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo");

            // Agregar opciones para el campo Sexo
            ViewBag.SexoOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Masculino", Text = "Masculino" },
                new SelectListItem { Value = "Femenino", Text = "Femenino" }
            };

            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,Cedula,Nombre,PrimerApellido,SegundoApellido,Sexo,FechaNacimiento,Edad,Email,Telefono,Direccion,FechaContratacion,PuestoId,SucursalId,Vacaciones,ID_Estado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.Edad = CalcularEdad(empleado.FechaNacimiento);

                empleado.Vacaciones = CalcularVacaciones(empleado.FechaContratacion);

                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto");
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion");
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo");

            // Volver a cargar las opciones para el campo Sexo en caso de que haya un error
            ViewBag.SexoOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Masculino", Text = "Masculino" },
                new SelectListItem { Value = "Femenino", Text = "Femenino" }
            };

            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Sucursal)
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);

            if (empleado == null)
            {
                return NotFound();
            }

            var viewModel = new EmpleadoCreateViewModel
            {
                EmpleadoId = empleado.EmpleadoId,
                Cedula = empleado.Cedula,
                Nombre = empleado.Nombre,
                PrimerApellido = empleado.PrimerApellido,
                SegundoApellido = empleado.SegundoApellido,
                Sexo = empleado.Sexo,
                FechaNacimiento = empleado.FechaNacimiento,
                Email = empleado.Email,
                Telefono = empleado.Telefono,
                Direccion = empleado.Direccion,
                FechaContratacion = empleado.FechaContratacion,
                PuestoId = empleado.PuestoId,
                SucursalId = empleado.SucursalId,
                Vacaciones = CalcularVacaciones(empleado.FechaContratacion),
                ID_Estado = empleado.ID_Estado,
                Departamento = _context.Puestos
                    .Where(p => p.PuestoId == empleado.PuestoId)
                    .Select(p => p.Departamento)
                    .FirstOrDefault(),
                SexoOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Masculino", Text = "Masculino" },
                    new SelectListItem { Value = "Femenino", Text = "Femenino" }
                }
            };

            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto", empleado.PuestoId);
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion", empleado.SucursalId);
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo", empleado.ID_Estado);

            return View(viewModel);
        }


        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Cedula,Nombre,PrimerApellido,SegundoApellido,Sexo,FechaNacimiento,Email,Telefono,Direccion,FechaContratacion,PuestoId,SucursalId,ID_Estado,Vacaciones")] EmpleadoCreateViewModel viewModel)
        {
            if (id != viewModel.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Mapear ViewModel a Entity
                    var empleado = await _context.Empleados.FindAsync(id);
                    if (empleado == null)
                    {
                        return NotFound();
                    }

                    // Actualizar datos del empleado
                    empleado.Cedula = viewModel.Cedula;
                    empleado.Nombre = viewModel.Nombre;
                    empleado.PrimerApellido = viewModel.PrimerApellido;
                    empleado.SegundoApellido = viewModel.SegundoApellido;
                    empleado.Sexo = viewModel.Sexo;
                    empleado.FechaNacimiento = viewModel.FechaNacimiento;
                    empleado.Email = viewModel.Email;
                    empleado.Telefono = viewModel.Telefono;
                    empleado.Direccion = viewModel.Direccion;
                    empleado.FechaContratacion = viewModel.FechaContratacion;
                    empleado.PuestoId = viewModel.PuestoId;
                    empleado.SucursalId = viewModel.SucursalId;
                    empleado.ID_Estado = viewModel.ID_Estado;
                    empleado.Edad = CalcularEdad(viewModel.FechaNacimiento);

                    // Guardar cambios en la base de datos
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Re-cargar datos necesarios para la vista
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto", viewModel.PuestoId);
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion", viewModel.SucursalId);
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo", viewModel.ID_Estado);

            return View(viewModel);
        }


        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Sucursal)
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.EmpleadoId == id);
        }

        // Calcular la edad
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var today = DateTime.Today;
            var age = today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > today.AddYears(-age)) age--;
            return age;
        }

        private int CalcularVacaciones(DateTime FechaContratacion)
        {
            // Calcula el número de meses entre la fecha de contratación y la fecha actual
            var mesesTrabajados = (DateTime.Now.Year - FechaContratacion.Year) * 12 + DateTime.Now.Month - FechaContratacion.Month;

            // 1 día de vacaciones por cada mes trabajado
            return mesesTrabajados;
        }
    }
}
