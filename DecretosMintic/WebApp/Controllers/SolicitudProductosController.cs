using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Controllers
{
    public class SolicitudProductosController : Controller
    {
        private readonly ReportesContext _context;

        public SolicitudProductosController(ReportesContext context)
        {
            _context = context;
        }

        // GET: SolicitudProductos
        public async Task<IActionResult> Index([FromQuery]string EstadoFilter, int? pageNumber)
        {
            ViewBag.EstadoFilter = String.IsNullOrEmpty(EstadoFilter) ? "" : EstadoFilter;

            var pagina = pageNumber == null ? 1 : pageNumber; 

            var tiposEstados = new List<SolicitudProductoEstado>();
            tiposEstados.Add(SolicitudProductoEstado.Aprobado);
            tiposEstados.Add(SolicitudProductoEstado.Negado);
            tiposEstados.Add(SolicitudProductoEstado.Pendiente);

            ViewBag.TiposEstados = tiposEstados;

            int pageSize = 10;

            if (String.IsNullOrEmpty(EstadoFilter)) {
                var solicitudes = _context.SolicitudProductos.Include(s => s.Persona).Include(s => s.Producto).OrderByDescending(s => s.Updated);
                return View(await PaginatedList<SolicitudProducto>.CreateAsync(solicitudes.AsNoTracking(), pagina ?? 1, pageSize));
            }

            var query = _context.SolicitudProductos
                .Include(s => s.Persona)
                .Include(s => s.Producto)
                .Where(sp => sp.Estado == EstadoFilter)
                .OrderByDescending(sp => sp.Updated);


            
            return View(await PaginatedList<SolicitudProducto>.CreateAsync(query.AsNoTracking(), pagina ?? 1, pageSize));

        }

        // GET: SolicitudProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudProducto = await _context.SolicitudProductos
                .Include(s => s.Persona)
                .Include(s => s.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudProducto == null)
            {
                return NotFound();
            }

            return View(solicitudProducto);
        }

        // GET: SolicitudProductos/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View();
        }

        // POST: SolicitudProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,UUID,Observaciones,PersonaId,ProductoId,Created,Updated")] SolicitudProducto solicitudProducto)
        {
            if (ModelState.IsValid)
            {
                solicitudProducto.Created = DateTime.Now;
                solicitudProducto.Updated = DateTime.Now;

                _context.Add(solicitudProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre", solicitudProducto.PersonaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", solicitudProducto.ProductoId);
            return View(solicitudProducto);
        }

        // GET: SolicitudProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tiposEstados = new List<SolicitudProductoEstado>();
            tiposEstados.Add(SolicitudProductoEstado.Aprobado);
            tiposEstados.Add(SolicitudProductoEstado.Negado);
            tiposEstados.Add(SolicitudProductoEstado.Pendiente);

            if (id == null)
            {
                return NotFound();
            }

            var solicitudProducto = await _context.SolicitudProductos.FindAsync(id);
            if (solicitudProducto == null)
            {
                return NotFound();
            }


            ViewData["Estado"] = new SelectList(tiposEstados,"Key", "Value",solicitudProducto.Estado);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre", solicitudProducto.PersonaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", solicitudProducto.ProductoId);
            return View(solicitudProducto);
        }

        // POST: SolicitudProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado,Observaciones,PersonaId,ProductoId, UUID, Created")] SolicitudProducto solicitudProducto)
        {
            if (id != solicitudProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    solicitudProducto.Updated = DateTime.Now;
                    _context.Update(solicitudProducto);

                    var producto = _context.Productos.Find(solicitudProducto.ProductoId);
                    producto.Updated = DateTime.Now;

                    if (String.Equals(solicitudProducto.Estado, SolicitudProductoEstado.Aprobado.Key))
                        producto.Estado = ProductoEstado.Aprobado.Key;
                    else
                        producto.Estado = ProductoEstado.NoAprobado.Key;

                    _context.Update(producto);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudProductoExists(solicitudProducto.Id))
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
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre", solicitudProducto.PersonaId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", solicitudProducto.ProductoId);
            return View(solicitudProducto);
        }

        // GET: SolicitudProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudProducto = await _context.SolicitudProductos
                .Include(s => s.Persona)
                .Include(s => s.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudProducto == null)
            {
                return NotFound();
            }

            return View(solicitudProducto);
        }

        // POST: SolicitudProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudProducto = await _context.SolicitudProductos.FindAsync(id);
            _context.SolicitudProductos.Remove(solicitudProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudProductoExists(int id)
        {
            return _context.SolicitudProductos.Any(e => e.Id == id);
        }
    }
}
