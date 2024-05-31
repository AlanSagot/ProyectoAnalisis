using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace LeahMakeUp.Controllers
{
    public class FacturasController : Controller
    {
        private readonly LeahDBContext _context;

        public FacturasController(LeahDBContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var leahDBContext = _context.Facturas.Include(f => f.Inventario).Include(f => f.Pago).Include(f => f.Usuario);
            return View(await leahDBContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Inventario)
                .Include(f => f.Pago)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto");
            ViewData["PagoId"] = new SelectList(_context.Pagos, "PagoId", "Detalle");
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "UsuarioId", "Cedula");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,FechaActual,Cantidad,Impuesto,ProductoId,UsuarioId,PagoId")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto", factura.ProductoId);
            ViewData["PagoId"] = new SelectList(_context.Pagos, "PagoId", "Detalle", factura.PagoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "UsuarioId", "Cedula", factura.UsuarioId);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto", factura.ProductoId);
            ViewData["PagoId"] = new SelectList(_context.Pagos, "PagoId", "Detalle", factura.PagoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "UsuarioId", "Cedula", factura.UsuarioId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,FechaActual,Cantidad,Impuesto,ProductoId,UsuarioId,PagoId")] Factura factura)
        {
            if (id != factura.FacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.FacturaId))
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
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto", factura.ProductoId);
            ViewData["PagoId"] = new SelectList(_context.Pagos, "PagoId", "Detalle", factura.PagoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "UsuarioId", "Cedula", factura.UsuarioId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Inventario)
                .Include(f => f.Pago)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.FacturaId == id);
        }
    }
}
