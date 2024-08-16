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
    public class CarritosController : Controller
    {
        private readonly LeahDBContext _context;

        public CarritosController(LeahDBContext context)
        {
            _context = context;
        }

        // GET: Carritos
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var userCedula = User.Identity.Name; // O usar Claims si es necesario

            // Filtrar los ítems del carrito por el usuario autenticado
            var leahDBContext = _context.Carritos
                .Include(c => c.Inventario)
                .Where(c => c.Cedula == userCedula);

            return View(await leahDBContext.ToListAsync());
        }


        // GET: Carritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Inventario)
                .FirstOrDefaultAsync(m => m.CarritoId == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "Codigo");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarritoId,Cedula,Cantidad,ProductoId,PrecioTotal")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "Codigo", carrito.ProductoId);
            return View(carrito);
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "Codigo", carrito.ProductoId);
            return View(carrito);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarritoId,Cedula,Cantidad,ProductoId")] Carrito carrito)
        {
            if (id != carrito.CarritoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalcula el PrecioTotal antes de actualizar el carrito
                    var producto = await _context.Inventarios.FindAsync(carrito.ProductoId);
                    carrito.PrecioTotal = carrito.Cantidad * producto.PrecioXVenta;

                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.CarritoId))
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
            ViewData["ProductoId"] = new SelectList(_context.Inventarios, "ProductoId", "Codigo", carrito.ProductoId);
            return View(carrito);
        }


        // GET: Carritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Inventario)
                .FirstOrDefaultAsync(m => m.CarritoId == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.CarritoId == id);
        }

        // POST: Carritos/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int cantidad = 1)
        {
            var producto = await _context.Inventarios.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            // Verifica si el producto tiene suficiente stock
            if (producto.Stock < cantidad)
            {
                // Puedes retornar un mensaje de error o redirigir a otra página
                return BadRequest("No hay suficiente stock disponible.");
            }
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Obtener la cédula del usuario autenticado
            var userCedula = User.Identity.Name; // O usar Claims si es necesario

            var carritoItem = await _context.Carritos
                .FirstOrDefaultAsync(c => c.ProductoId == id && c.Cedula == userCedula);

            if (carritoItem != null)
            {
                // Si el producto ya está en el carrito, solo actualiza la cantidad
                carritoItem.Cantidad += cantidad;
                carritoItem.PrecioTotal = carritoItem.Cantidad * producto.PrecioXVenta;
                _context.Update(carritoItem);
            }
            else
            {
                // Si no está, agrega un nuevo ítem al carrito
                var nuevoCarritoItem = new Carrito
                {
                    ProductoId = id,
                    Cantidad = cantidad,
                    PrecioTotal = producto.PrecioXVenta * cantidad,
                    Cedula = userCedula
                };
                _context.Add(nuevoCarritoItem);
            }

            // Descontar el stock del inventario
            producto.Stock -= cantidad;
            _context.Update(producto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        // POST: Carritos/UpdateQuantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int nuevaCantidad)
        {
            var carritoItem = await _context.Carritos
                                            .Include(c => c.Inventario)
                                            .FirstOrDefaultAsync(c => c.CarritoId == id);

            if (carritoItem == null || carritoItem.Inventario == null || nuevaCantidad < 1)
            {
                return BadRequest("No se pudo encontrar el carrito o el inventario, o la cantidad nueva no es válida.");
            }

            int diferenciaCantidad = nuevaCantidad - carritoItem.Cantidad;

            if (carritoItem.Inventario.Stock < diferenciaCantidad)
            {
                return Json(new { success = false, message = "No hay suficiente stock disponible." });
            }

            carritoItem.Inventario.Stock -= diferenciaCantidad;
            carritoItem.Cantidad = nuevaCantidad;
            carritoItem.PrecioTotal = carritoItem.Cantidad * carritoItem.Inventario.PrecioXVenta;

            _context.Update(carritoItem);
            _context.Update(carritoItem.Inventario);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // POST: Carritos/RemoveFromCart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var carritoItem = await _context.Carritos
            .Include(c => c.Inventario) // Incluye el inventario para actualizar el stock
            .FirstOrDefaultAsync(c => c.CarritoId == id);

            if (carritoItem == null)
            {
                return NotFound();
            }

            // Actualizar el stock del producto en el inventario
            if (carritoItem.Inventario != null)
            {
                carritoItem.Inventario.Stock += carritoItem.Cantidad;
                _context.Update(carritoItem.Inventario);
            }

            // Eliminar el ítem del carrito
            _context.Carritos.Remove(carritoItem);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // POST: Carritos/ClearCart
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userCedula = User.Identity.Name;

            if (string.IsNullOrEmpty(userCedula))
            {
                return Unauthorized();
            }

            // Obtener todos los ítems del carrito del usuario
            var carritos = await _context.Carritos
                .Where(c => c.Cedula == userCedula)
                .Include(c => c.Inventario)  // Incluir el inventario para actualizar el stock
                .ToListAsync();

            // Actualizar el stock de cada producto
            foreach (var carrito in carritos)
            {
                if (carrito.Inventario != null)
                {
                    carrito.Inventario.Stock += carrito.Cantidad;
                    _context.Update(carrito.Inventario);
                }
            }

            // Eliminar todos los ítems del carrito
            _context.Carritos.RemoveRange(carritos);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
