using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ReportesContext _context;

        public ProductosController(ReportesContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index([FromQuery] string EstadoFilter, int? pageNumber)
        {
            ViewBag.EstadoFilter = String.IsNullOrEmpty(EstadoFilter) ? "" : EstadoFilter;
            var pagina = pageNumber == null ? 1 : pageNumber;

            var tiposEstados = new List<ProductoEstado>();
            tiposEstados.Add(ProductoEstado.Aprobado);
            tiposEstados.Add(ProductoEstado.NoAprobado);

            ViewBag.TiposEstados = tiposEstados;

            int pageSize = 10;

            if (String.IsNullOrEmpty(EstadoFilter))
            {
                var productos = _context.Productos.OrderByDescending(p=> p.Updated);
                return View(await PaginatedList<Producto>.CreateAsync(productos.AsNoTracking(), pagina ?? 1, pageSize));
            }

            var query = _context.Productos
                .Where(p => p.Estado == EstadoFilter)
                .OrderByDescending( p=> p.Updated);

            return View(await PaginatedList<Producto>.CreateAsync(query.AsNoTracking(), pagina ?? 1, pageSize));
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Url,Estado,Created,Updated")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Created = DateTime.Now;
                producto.Updated = DateTime.Now;

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tiposEstados = new List<ProductoEstado>();
            tiposEstados.Add(ProductoEstado.Aprobado);
            tiposEstados.Add(ProductoEstado.NoAprobado);

            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            ViewData["Estado"] = new SelectList(tiposEstados, "Key", "Value", producto.Estado);

            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Url,Estado,Created,Updated")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.Updated = DateTime.Now;
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }


        [HttpGet]
        [AllowAnonymous]
        public JsonResult Consulta([FromQuery] string query)
        {
            var resultados = _context.Productos
                .Where(p => p.Nombre.ToLower().Contains(query.ToLower()))
                .Select(p => new { 
                    id = p.Id,
                    nombre = p.Nombre
                });

            var response = new { 
                items = resultados
            };

            return new JsonResult(response);
        }
    }
}
