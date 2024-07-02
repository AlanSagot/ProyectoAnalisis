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
        public IActionResult Maquillaje()
        {
            var productosMaquillaje = _context.Inventarios
                                        .Where(i => i.Categoria.Equals("Maquillaje"))
                                        .ToList();

            ViewBag.Categoria = "Maquillaje";
            return View("Maquillaje", productosMaquillaje);
        }

 
        // GET: Catalogos/Paletas
        public IActionResult SkinCare()
        {
            var productosSkinCare = _context.Inventarios
                                        .Where(i => i.Categoria.Equals("SkinCare"))
                                        .ToList();

            ViewBag.Categoria = "SkinCare"; 
            return View("SkinCare", productosSkinCare);
        }


        // GET: Catalogos/Sombras
        public IActionResult Articulos()
        {
            var productosArticulos = _context.Inventarios
                                       .Where(i => i.Categoria.Equals("Articulos"))
                                       .ToList();

            ViewBag.Categoria = "Articulos"; 
            return View("Articulos", productosArticulos);
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
