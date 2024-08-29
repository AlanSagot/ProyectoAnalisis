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
using iText.Kernel.Exceptions;
using iText.Kernel.Geom;


namespace LeahMakeUp.Controllers
{
    public class InventariosController : Controller
    {
        private readonly LeahDBContext _context;

        public InventariosController(LeahDBContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Productos()
        {
            var productos = await _context.Inventarios.Include(i => i.Estado)
                                          .Where(i => i.Estado.Tipo.Equals("Activo")).ToListAsync();
            return View(productos);
        }
        public async Task<IActionResult> SkinCare()
        {
            var productos = await _context.Inventarios
                                          .Include(i => i.Categoria)
                                          .Where(i => i.Categoria.Descripcion.Equals("SkinCare"))
                                          .Include(i => i.Estado)
                                          .Where(i => i.Estado.Tipo.Equals("Activo"))
                                          .ToListAsync();

            ViewBag.Categoria = "SkinCare";
            return View("SkinCare", productos);
        }

        public async Task<IActionResult> Articulos()
        {
            var productos = await _context.Inventarios
                                          .Include(i => i.Categoria) // Incluye la relación con la categoría
                                          .Where(i => i.Categoria.Descripcion.Equals("Artículos"))
                                          .Include(i => i.Estado)
                                          .Where(i => i.Estado.Tipo.Equals("Activo"))
                                          .ToListAsync();

            ViewBag.Categoria = "Articulos";
            return View("Articulos", productos);
        }

        public async Task<IActionResult> Maquillaje()
        {
            var productos = await _context.Inventarios
                                          .Include(i => i.Categoria) // Incluye la relación con la categoría
                                          .Where(i => i.Categoria.Descripcion.Equals("Maquillaje"))
                                          .Include(i => i.Estado)
                                          .Where(i => i.Estado.Tipo.Equals("Activo"))
                                          .ToListAsync();

            ViewBag.Categoria = "Maquillaje";
            return View("Maquillaje", productos);
        }


        //Método para generar informe PDF
        public async Task<IActionResult> GenerarInformePDF(DateTime fechaInicio, DateTime fechaFin, string categoria)
        {
            try
            {

                var inventarios = await _context.Inventarios
                    .Include(i => i.Sucursal)
                    .Include(i => i.Categoria)
                    .Include(i => i.Marca)
                    .Where(i => i.FechaAgregado >= fechaInicio && i.FechaAgregado <= fechaFin && (string.IsNullOrEmpty(categoria)))
                    .ToListAsync();


                string tempFilePath = System.IO.Path.GetTempFileName();

                using (var writer = new PdfWriter(tempFilePath))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var pageSize = PageSize.A4.Rotate();
                        using (var document = new Document(pdf, pageSize))
                        {

                            document.Add(new Paragraph("Informe de Inventario"));
                            document.Add(new Paragraph($"Fecha: {DateTime.Now}"));
                            document.Add(new Paragraph(" "));

                            var table = new Table(8);

                            table.AddCell("ID del Producto");
                            table.AddCell("Nombre del Producto");
                            table.AddCell("Categoría");
                            table.AddCell("Descripción del Producto");
                            table.AddCell("Marca");
                            table.AddCell("Precio de Venta");
                            table.AddCell("Stock");
                            table.AddCell("Sucursal");



                            foreach (var item in inventarios)
                            {
                                table.AddCell(item.ProductoId.ToString());
                                table.AddCell(item.NombreProducto);
                                table.AddCell(item.Categoria.Descripcion);
                                table.AddCell(item.DescripcionProducto);
                                table.AddCell(item.Marca.Descripcion);
                                table.AddCell(item.PrecioXVenta.ToString());
                                table.AddCell(item.Stock.ToString());
                                table.AddCell(item.Sucursal.Direccion);

                            }

                            document.Add(table);
                        }

                    }


                }

                var pdfBytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);


                System.IO.File.Delete(tempFilePath);

                if (pdfBytes.Length > 0)
                {
                    return File(pdfBytes, "application/pdf", "Informe_Inventario.pdf");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al generar el PDF. El archivo está vacío.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al generar el informe: {ex.Message}");
            }
        }

        //GET: Inventarios
        public async Task<IActionResult> Index()
        {
            var leahDBContext = _context.Inventarios
                                        .Include(i => i.Sucursal)
                                        .Include(i => i.Categoria)
                                        .Include(i => i.Marca);

            return View(await leahDBContext.ToListAsync());
        }






        // GET: Inventarios/Create
        public IActionResult Create()
        {
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion");
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo");
            ViewData["ID_Categoria"] = new SelectList(_context.Categorias, "ID_Categoria", "Descripcion");
            ViewData["ID_Marca"] = new SelectList(_context.Marcas, "ID_Marca", "Descripcion");

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
                    ID_Categoria = inventario.ID_Categoria,
                    DescripcionProducto = inventario.DescripcionProducto,
                    ID_Marca = inventario.ID_Marca,
                    PrecioXVenta = inventario.PrecioXVenta,
                    PrecioXCosto = inventario.PrecioXCosto,
                    Stock = inventario.Stock,
                    FechaAgregado = DateTime.Now,
                    FechaExpiracion = inventario.FechaExpiracion,
                    SucursalId = inventario.SucursalId,
                    ID_Estado = inventario.ID_Estado
                };

                _context.Add(inv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "SucursalId", "Direccion", inventario.SucursalId);
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo", inventario.ID_Estado);
            ViewData["ID_Categoria"] = new SelectList(_context.Categorias, "ID_Categoria", "Descripcion", inventario.ID_Categoria);
            ViewData["ID_Marca"] = new SelectList(_context.Marcas, "ID_Marca", "Descripcion", inventario.ID_Marca);
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
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo", inventario.ID_Estado);
            ViewData["ID_Categoria"] = new SelectList(_context.Categorias, "ID_Categoria", "Descripcion", inventario.ID_Categoria);
            ViewData["ID_Marca"] = new SelectList(_context.Marcas, "ID_Marca", "Descripcion", inventario.ID_Marca);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Codigo,NombreProducto,ID_Categoria,DescripcionProducto,ID_Marca,PrecioXVenta,PrecioXCosto,Stock,FechaAgregado,FechaExpiracion,SucursalId,ID_Estado")] Inventario inventario, IFormFile FotoProducto)
        {
            if (id != inventario.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el inventario existente
                    var inventarioExistente = await _context.Inventarios.FindAsync(id);

                    if (inventarioExistente == null)
                    {
                        return NotFound();
                    }

                    // Si se proporciona una nueva imagen, usarla; si no, mantener la imagen existente
                    if (FotoProducto != null && FotoProducto.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await FotoProducto.CopyToAsync(memoryStream);
                            inventario.FotoProducto = memoryStream.ToArray();
                        }
                    }
                    else
                    {
                        // Si no se proporciona nueva imagen, conservar la imagen existente
                        inventario.FotoProducto = inventarioExistente.FotoProducto;
                    }

                    // Actualizar el inventario con los datos nuevos
                    _context.Entry(inventarioExistente).CurrentValues.SetValues(inventario);
                    _context.Entry(inventarioExistente).Property(x => x.FotoProducto).CurrentValue = inventario.FotoProducto;
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
            ViewData["ID_Estado"] = new SelectList(_context.Estados, "ID_Estado", "Tipo", inventario.ID_Estado);
            ViewData["ID_Categoria"] = new SelectList(_context.Categorias, "ID_Categoria", "Descripcion", inventario.ID_Categoria);
            ViewData["ID_Marca"] = new SelectList(_context.Marcas, "ID_Marca", "Descripcion", inventario.ID_Marca);
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
                .Include(i => i.Categoria)
                .Include(i => i.Marca)
                .Include(i => i.Estado)
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
            var productos = from p in _context.Inventarios.Include(i => i.Sucursal)
                            .Include(i => i.Categoria)
                            .Include(i => i.Marca)
                            .Include(i => i.Estado)
                            select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.NombreProducto.Contains(searchString));
            }

            // Limpiar el campo de búsqueda después de procesar la solicitud
            ViewData["SearchString"] = string.Empty;

            return View(await productos.ToListAsync());
        }


    }
}
