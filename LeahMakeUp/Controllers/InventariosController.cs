using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using LeahMakeUp.Models;

namespace LeahMakeUp.Controllers
{
    public class InventariosController : Controller
    {
        private readonly LeahDBContext _context;

        public InventariosController(LeahDBContext context)
        {
            _context = context;
        }

        // Método para generar informe PDF
        public async Task<IActionResult> GenerarInformePDF(DateTime fechaInicio, DateTime fechaFin, string categoria)
        {
            try
            {
                var inventarios = await _context.Inventarios
                    .Include(i => i.Sucursal)
                    .Where(i => i.FechaAgregado >= fechaInicio && i.FechaAgregado <= fechaFin && (string.IsNullOrEmpty(categoria) || i.Categoria == categoria))
                    .ToListAsync();

                using (var ms = new MemoryStream())
                {
                    var writer = new PdfWriter(ms);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);

                    // Título del documento
                    document.Add(new Paragraph("Informe de Inventario"));
                    document.Add(new Paragraph($"Fecha: {DateTime.Now}"));
                    document.Add(new Paragraph(" "));

                    // Tabla de inventario
                    var table = new Table(10);
                    table.AddCell("ProductoId");
                    table.AddCell("NombreProducto");
                    table.AddCell("Categoria");
                    table.AddCell("DescripcionProducto");
                    table.AddCell("Marca");
                    table.AddCell("PrecioXVenta");
                    table.AddCell("PrecioXCosto");
                    table.AddCell("Stock");
                    table.AddCell("FechaAgregado");
                    table.AddCell("FechaExpiracion");

                    foreach (var item in inventarios)
                    {
                        table.AddCell(item.ProductoId.ToString());
                        table.AddCell(item.NombreProducto);
                        table.AddCell(item.Categoria);
                        table.AddCell(item.DescripcionProducto);
                        table.AddCell(item.Marca);
                        table.AddCell(item.PrecioXVenta.ToString());
                        table.AddCell(item.PrecioXCosto.ToString());
                        table.AddCell(item.Stock.ToString());
                        table.AddCell(item.FechaAgregado.ToString("dd/MM/yyyy"));
                        table.AddCell(item.FechaExpiracion.ToString("dd/MM/yyyy"));
                    }

                    document.Add(table);
                    document.Close();

                    return File(ms.ToArray(), "application/pdf", "Informe_Inventario.pdf");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al generar el informe: {ex.Message}");
            }
        }


        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
            var leahDBContext = _context.Inventarios.Include(i => i.Sucursal);
            return View(await leahDBContext.ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                .Include(i => i.Sucursal)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }


        // GET: Inventarios/Create
        public IActionResult Create()
        {
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion");
            return View();
        }

        // POST: Inventarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventarioCreateViewModel inventario, IFormFile FotoProducto)
        {
            if (ModelState.IsValid)
            {
                byte[]? imagen = null;

                if (FotoProducto != null && FotoProducto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await FotoProducto.CopyToAsync(memoryStream);
                        imagen = memoryStream.ToArray();
                    }
                }

                Inventario inv = new Inventario
                {
                    ProductoId = inventario.ProductoId,
                    Codigo = inventario.Codigo,
                    FotoProducto = imagen,
                    NombreProducto = inventario.NombreProducto,
                    Categoria = inventario.Categoria,
                    DescripcionProducto = inventario.DescripcionProducto,
                    Marca = inventario.Marca,
                    PrecioXVenta = inventario.PrecioXVenta,
                    PrecioXCosto = inventario.PrecioXCosto,
                    FechaAgregado = DateTime.Now,
                    FechaExpiracion = inventario.FechaExpiracion,
                    SucursalId = inventario.SucursalId
                };

                _context.Add(inv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion", inventario.SucursalId);
            return View(inventario);
        }



        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion", inventario.SucursalId);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,NombreProducto,Categoria,DescripcionProducto,Marca,PrecioXVenta,PrecioXCosto,Stock,FechaAgregado,FechaExpiracion,SucursalId")] Inventario inventario, IFormFile FotoProducto)
        {
            if (id != inventario.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                byte[]? imagen = null;

                if (FotoProducto != null && FotoProducto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await FotoProducto.CopyToAsync(memoryStream);
                        imagen = memoryStream.ToArray();
                    }
                }
                else
                {
                    imagen = inventario.FotoProducto;
                }

                try
                {
                    inventario.FotoProducto = imagen;
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.ProductoId))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion", inventario.SucursalId);
            return View(inventario);
        }

        // GET: Inventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                .Include(i => i.Sucursal)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario != null)
            {
                _context.Inventarios.Remove(inventario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventarios.Any(e => e.ProductoId == id);
        }

        //Barra de Búsqueda
        [Route("Inventarios/Index")]
        public async Task<IActionResult> Index(string searchString)
        {
            var productos = from p in _context.Inventarios
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.NombreProducto.Contains(searchString));
            }

            return View(await productos.ToListAsync());
        }

    }
}
