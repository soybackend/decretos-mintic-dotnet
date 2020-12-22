using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Controllers
{
    public class CursosController : Controller
    {
        private readonly ReportesContext _context;

        public CursosController(ReportesContext context)
        {
            _context = context;
        }

        // GET: Cursos
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
                var cursos = _context.Cursos.OrderByDescending(p => p.Updated);
                return View(await PaginatedList<Curso>.CreateAsync(cursos.AsNoTracking(), pagina ?? 1, pageSize));
            }

            var query = _context.Cursos
                .Where(p => p.Estado == EstadoFilter)
                .OrderByDescending(p => p.Updated);

            return View(await PaginatedList<Curso>.CreateAsync(query.AsNoTracking(), pagina ?? 1, pageSize));
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Url,Estado,Created,Updated")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                curso.Created = DateTime.Now;
                curso.Updated = DateTime.Now;

                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var tiposEstados = new List<CursoEstado>();
            tiposEstados.Add(CursoEstado.Aprobado);
            tiposEstados.Add(CursoEstado.NoAprobado);

            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            ViewData["Estado"] = new SelectList(tiposEstados, "Key", "Value", curso.Estado);

            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Url,Estado,Created,Updated")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    curso.Updated = DateTime.Now;
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
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
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
