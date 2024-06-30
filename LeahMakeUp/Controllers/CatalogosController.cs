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
    public class CatalogosController : Controller
    {
        private readonly LeahDBContext _context;

        public CatalogosController(LeahDBContext context)
        {
            _context = context;
        }

        // GET: Catalogos
        public async Task<IActionResult> Index()
        {
            var leahDBContext = _context.Catalogos.Include(c => c.Inventario);
            return View(await leahDBContext.ToListAsync());
        }
     
/*****************************************/
        // GET: Catalogos/Labial
        public IActionResult Labiales()
        {
            var productosLabial = _context.Inventarios
                                        .Where(i => i.Categoria.Equals("Labiales"))
                                        .ToList();

            ViewBag.Categoria = "Labiales";
            return View("Labiales", productosLabial);
        }

        // GET: Catalogos/LabialesAgregar
        public async Task<IActionResult> LabialesAgregar()
        {
            var productosLabial = await _context.Inventarios
                .Where(i => i.Categoria.Equals("Labiales"))
                .ToListAsync();

        var productosEnCatalogo = await _context.Catalogos
            .Where(c => c.NombreCatalogo.Equals("Labiales"))
            .Select(c => c.ProductoId)
            .ToListAsync();

        ViewBag.ProductosEnCatalogo = productosEnCatalogo;

            return View(productosLabial);
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LabialesAgregar(List<int> productosIds)
        {
            var productosEnCatalogo = await _context.Catalogos
                .Where(c => c.NombreCatalogo.Equals("Labiales"))
                .Select(c => c.ProductoId)
                .ToListAsync();

            // Agregar productos nuevos
            foreach (var productoId in productosIds.Except(productosEnCatalogo))
            {
                var catalogo = new Catalogos
                {
                    NombreCatalogo = "Labiales",
                    DetalleCatalogo = "Producto añadido a Labiales",
                    ProductoId = productoId
                };
                _context.Catalogos.Add(catalogo);
            }

            // Quitar productos que ya no están seleccionados
            foreach (var productoId in productosEnCatalogo.Except(productosIds))
            {
                var catalogo = await _context.Catalogos
                    .FirstOrDefaultAsync(c => c.NombreCatalogo.Equals("Labiales") && c.ProductoId == productoId);
                if (catalogo != null)
                {
                    _context.Catalogos.Remove(catalogo);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Labiales));
        }

        /**********************************************/

        // GET: Catalogos/Paletas
        public IActionResult Paletas()
        {
            var productosPaleta = _context.Inventarios
                                        .Where(i => i.Categoria.Equals("Paletas"))
                                        .ToList();

            ViewBag.Categoria = "Paletas"; 
            return View("Paletas", productosPaleta);
        }


        // GET: Catalogos/Sombras
        public IActionResult Sombras()
        {
            var productosSombra = _context.Inventarios
                                       .Where(i => i.Categoria.Equals("Sombras"))
                                       .ToList();

            ViewBag.Categoria = "Sombras"; 
            return View("Sombras", productosSombra);
        }




        // GET: Catalogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogos = await _context.Catalogos
                .Include(c => c.Inventario)
                .FirstOrDefaultAsync(m => m.CatalogoId == id);
            if (catalogos == null)
            {
                return NotFound();
            }

            return View(catalogos);
        }

        // GET: Catalogos/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto");
            return View();
        }

        // POST: Catalogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogoId,NombreCatalogo,DetalleCatalogo,ProductoId")] Catalogos catalogos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto", catalogos.ProductoId);
            return View(catalogos);
        }

        // GET: Catalogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogos = await _context.Catalogos.FindAsync(id);
            if (catalogos == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto", catalogos.ProductoId);
            return View(catalogos);
        }

        // POST: Catalogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatalogoId,NombreCatalogo,DetalleCatalogo,ProductoId")] Catalogos catalogos)
        {
            if (id != catalogos.CatalogoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogosExists(catalogos.CatalogoId))
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
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "NombreProducto", catalogos.ProductoId);
            return View(catalogos);
        }

        // GET: Catalogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogos = await _context.Catalogos
                .Include(c => c.Inventario)
                .FirstOrDefaultAsync(m => m.CatalogoId == id);
            if (catalogos == null)
            {
                return NotFound();
            }

            return View(catalogos);
        }

        // POST: Catalogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogos = await _context.Catalogos.FindAsync(id);
            if (catalogos != null)
            {
                _context.Catalogos.Remove(catalogos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogosExists(int id)
        {
            return _context.Catalogos.Any(e => e.CatalogoId == id);
        }
    }
}
