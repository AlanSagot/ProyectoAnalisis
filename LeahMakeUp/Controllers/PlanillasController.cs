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

        // GET: Planillas/Create
        public IActionResult Create()
        {
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto");
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion"); 
            return View();
        }

        // POST: Planillas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanillasCreateViewModel planillas)
        {
            if (ModelState.IsValid)
            {
                var puesto = await _context.Puestos.FindAsync(planillas.PuestoId);
                if (puesto == null)
                {
                    return NotFound();
                }

                planillas.Departamento = puesto.Departamento;
                planillas.NombrePuesto = puesto.NombrePuesto;

                

                Empleado newEmpleado = new Empleado
                {
                    EmpleadoId = planillas.Cedula,
                    Nombre = planillas.Nombre,
                    PrimerApellido = planillas.PrimerApellido,
                    SegundoApellido = planillas.SegundoApellido,
                    PuestoId = planillas.PuestoId,
                    SucursalId = planillas.SucursalId,
                };

                Planillas newPlanilla = new Planillas
                {
                    EmpleadoId = planillas.Cedula,
                    PuestoId = planillas.PuestoId,
                    SucursalId = planillas.SucursalId,
                };

                _context.Planillas.Add(newPlanilla);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto");
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion"); 
            return View(planillas);
        }


        // GET: Planillas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planillas = await _context.Planillas.FindAsync(id);
            if (planillas == null)
            {
                return NotFound();
            }
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto");
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion"); 
            return View(planillas);
        }

        // POST: Planillas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanillaId,EmpleadoId,PuestoId,SucursalId,Salario,FechaIngreso")] Planillas planillas)
        {
            if (id != planillas.PlanillaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planillas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillasExists(planillas.PlanillaId))
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
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "NombrePuesto");
            ViewData["Departamento"] = new SelectList(_context.Puestos.Select(p => p.Departamento).Distinct());
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion"); 
            return View(planillas);
        }

        // GET: Planillas/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planillas = await _context.Planillas.FindAsync(id);
            if (planillas != null)
            {
                _context.Planillas.Remove(planillas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanillasExists(int id)
        {
            return _context.Planillas.Any(e => e.PlanillaId == id);
        }
    }
}
