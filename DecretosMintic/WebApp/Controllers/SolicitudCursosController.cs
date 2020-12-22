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
    public class SolicitudCursosController : Controller
    {
        private readonly ReportesContext _context;

        public SolicitudCursosController(ReportesContext context)
        {
            _context = context;
        }

        // GET: SolicitudCursos
        public async Task<IActionResult> Index([FromQuery] string EstadoFilter, int? pageNumber)
        {
            ViewBag.EstadoFilter = String.IsNullOrEmpty(EstadoFilter) ? "" : EstadoFilter;

            var pagina = pageNumber == null ? 1 : pageNumber;

            var tiposEstados = new List<SolicitudProductoEstado>();
            tiposEstados.Add(SolicitudProductoEstado.Aprobado);
            tiposEstados.Add(SolicitudProductoEstado.Negado);
            tiposEstados.Add(SolicitudProductoEstado.Pendiente);

            ViewBag.TiposEstados = tiposEstados;

            var reportesContext = _context.SolicitudCursos.Include(s => s.Curso).Include(s => s.Persona);

            int pageSize = 10;

            if (String.IsNullOrEmpty(EstadoFilter))
            {
                var solicitudes = _context.SolicitudCursos.Include(s => s.Persona).Include(s => s.Curso).OrderByDescending(s => s.Updated);
                return View(await PaginatedList<SolicitudCurso>.CreateAsync(solicitudes.AsNoTracking(), pagina ?? 1, pageSize));
            }

            var query = _context.SolicitudCursos
                .Include(s => s.Persona)
                .Include(s => s.Curso)
                .Where(sp => sp.Estado == EstadoFilter)
                .OrderByDescending(sp => sp.Updated);

            return View(await PaginatedList<SolicitudCurso>.CreateAsync(query.AsNoTracking(), pagina ?? 1, pageSize));
        }

        // GET: SolicitudCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudCurso = await _context.SolicitudCursos
                .Include(s => s.Curso)
                .Include(s => s.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudCurso == null)
            {
                return NotFound();
            }

            return View(solicitudCurso);
        }

        // GET: SolicitudCursos/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre");
            return View();
        }

        // POST: SolicitudCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,UUID,Observaciones,PersonaId,CursoId,Created,Updated")] SolicitudCurso solicitudCurso)
        {
            if (ModelState.IsValid)
            {
                solicitudCurso.Created = DateTime.Now;
                solicitudCurso.Updated = DateTime.Now;

                _context.Add(solicitudCurso);

                solicitudCurso.UUID = Guid.NewGuid();

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", solicitudCurso.CursoId);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre", solicitudCurso.PersonaId);
            return View(solicitudCurso);
        }

        // GET: SolicitudCursos/Edit/5
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

            var solicitudCurso = await _context.SolicitudCursos.FindAsync(id);
            if (solicitudCurso == null)
            {
                return NotFound();
            }

            ViewData["Estado"] = new SelectList(tiposEstados, "Key", "Value", solicitudCurso.Estado);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", solicitudCurso.CursoId);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre", solicitudCurso.PersonaId);
            
            return View(solicitudCurso);
        }

        // POST: SolicitudCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado,UUID,Observaciones,PersonaId,CursoId,Created")] SolicitudCurso solicitudCurso)
        {

            if (id != solicitudCurso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    solicitudCurso.Updated = DateTime.Now;
                    solicitudCurso.Created = DateTime.Now;

                    _context.Update(solicitudCurso);

                    var curso = _context.Cursos.Find(id=solicitudCurso.CursoId);
                    curso.Updated = DateTime.Now;

                    if (String.Equals(solicitudCurso.Estado, SolicitudProductoEstado.Aprobado.Key))
                        curso.Estado = CursoEstado.Aprobado.Key;
                    else
                        curso.Estado = CursoEstado.NoAprobado.Key;

                    _context.Update(curso);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudCursoExists(solicitudCurso.Id))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", solicitudCurso.CursoId);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "Id", "Nombre", solicitudCurso.PersonaId);
            return View(solicitudCurso);
        }

        // GET: SolicitudCursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudCurso = await _context.SolicitudCursos
                .Include(s => s.Curso)
                .Include(s => s.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudCurso == null)
            {
                return NotFound();
            }

            return View(solicitudCurso);
        }

        // POST: SolicitudCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudCurso = await _context.SolicitudCursos.FindAsync(id);
            _context.SolicitudCursos.Remove(solicitudCurso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudCursoExists(int id)
        {
            return _context.SolicitudCursos.Any(e => e.Id == id);
        }
    }
}
