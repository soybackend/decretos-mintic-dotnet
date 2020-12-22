using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

using WebApp.Utils;
using Wkhtmltopdf.NetCore;
using FluentDateTime;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{

    public class PersonasController : Controller
    {
        private readonly ReportesContext _context;

        private SolicitudSoftware _solicitudSoftware;
        private SolicitudCourse _solicitudCurso;
        readonly IGeneratePdf _generatePdf;
        private IConfiguration _config;

        private EmailSender _email;

        public PersonasController(ReportesContext context, IGeneratePdf generatePdf, IConfiguration configuration)
        {
            _context = context;
            _solicitudSoftware = new SolicitudSoftware(context);
            _solicitudCurso = new SolicitudCourse(context);
            _generatePdf = generatePdf;
            _email = new EmailSender();

            _config = configuration;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personas.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            var tiposPersona = new List<TipoPersona>();
            tiposPersona.Add(TipoPersona.Natural);
            tiposPersona.Add(TipoPersona.Juridica);

            ViewBag.TiposPersona = tiposPersona;

            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoPersona,Nombre,TipoIdentificacion,NumeroIdentificacion,Correo,Telefono,Created,Updated")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.Created = DateTime.Now;
                persona.Updated = DateTime.Now;
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoPersona,Nombre,TipoIdentificacion,NumeroIdentificacion,Correo,Telefono,Created,Updated")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    persona.Updated = DateTime.Now;
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult DatosPersonas([FromQuery] string id)
        {
            var personas = _context.Personas
                .Where(b => b.NumeroIdentificacion == id)
                .ToList();

            return new JsonResult(personas);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult ConsultaCursos([FromQuery] string id) {

            var cursos = from c in _context.Cursos
                         join sc in _context.SolicitudCursos
                         on c.Id equals sc.CursoId
                         join p in _context.Personas
                         on sc.PersonaId equals p.Id
                         where sc.Estado == SolicitudProductoEstado.Aprobado.Value &&
                         p.NumeroIdentificacion == id
                         select new {
                             c.Nombre,
                             sc.UUID
                         };

            return new JsonResult(cursos);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Solicitar([Bind("NumeroIdentificacion", "TipoIdentificacion", "TipoPersona", "Nombre", "Correo", "Telefono", "TipoCertificado", "VerificarCheck", "Producto", "NombreProducto", "UrlProducto", "NombreCurso", "UrlCurso")] SolicitudViewModel solicitud) {

            using var transaction =  _context.Database.BeginTransaction();
            try
            {

                if (solicitud.NumeroIdentificacion == "")
                    return NotFound();

                var persona = _context.Personas
                    .Where(p => p.NumeroIdentificacion == solicitud.NumeroIdentificacion)
                    .ToList();

                //Si la persona no existe la crea
                var personaId = persona.Count > 0 ? persona[0].Id : CrearPersona(solicitud);

                //El usuario solicita un certificado de software
                if (solicitud.TipoCertificado == TipoCertificado.Software.Value)
                {
                   

                    var person = _context.Personas.Find(personaId);

                    //Si es un certificado de un software que existe
                    if (!solicitud.VerificarCheck)
                    {

                        var certificado = _solicitudSoftware.Solicitar(solicitud, personaId);

                        var host = _config.GetConnectionString("Host");

                        var _generadorPDF = new GeneradorPDF(_generatePdf, host);

                        return new FileStreamResult(_generadorPDF.Generar(person, certificado), "application/pdf");
                    }

                    var solicitudCreada = CrearSolicitudProducto(solicitud, personaId);
                    transaction.Commit();

                    _email.EnviarNotifProducto(solicitudCreada, person);

                    return RedirectToAction("ResultadoSolicitudProducto", new { uuid = solicitudCreada.UUID });
                }
                else {
                    var solicitudCursoCreada = _solicitudCurso.Solicitar(solicitud, personaId);
                    transaction.Commit();

                    return RedirectToAction("ResultadoSolicitudCurso", new { uuid = solicitudCursoCreada.UUID });
                }
            }
            catch (Exception e) {
                return NotFound(e.Message);
            }
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResultadoSolicitudProducto([FromQuery] string uuid) {

            var solicitudCreada = _context.SolicitudProductos
                .Where(s => s.UUID.ToString() == uuid)
                .Include(solicitud => solicitud.Producto)
                .ToList();

            if (solicitudCreada.Count == 0)
                return NotFound();

            var solicitudItem = solicitudCreada[0];

            ViewBag.NombreProducto = solicitudItem.Producto.Nombre;
            ViewBag.EstadoTipo = solicitudItem.Estado;
            ViewBag.Estado = SolicitudProductoEstado.TraerNombreTipo(solicitudItem.Estado);

            var fecha = DateTime.Now.AddBusinessDays(1);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            ViewBag.Fecha = fecha.ToLongDateString();

            ViewBag.Tipo = "producto";


            return View("~/Views/Home/SolicitudDetail.cshtml");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResultadoSolicitudCurso([FromQuery] string uuid)
        {
            var solicitudCreada = _context.SolicitudCursos
                .Where(s => s.UUID.ToString() == uuid)
                .Include(solicitud => solicitud.Curso)
                .ToList();

            if (solicitudCreada.Count == 0)
                return NotFound();

            var solicitudItem = solicitudCreada[0];

            ViewBag.NombreCurso = solicitudItem.Curso.Nombre;
            ViewBag.EstadoTipo = solicitudItem.Estado;
            ViewBag.Estado = SolicitudProductoEstado.TraerNombreTipo(solicitudItem.Estado);

            var fecha = DateTime.Now.AddBusinessDays(1);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            ViewBag.Fecha = fecha.ToLongDateString();

            ViewBag.Tipo = "curso";

            return View("~/Views/Home/SolicitudDetail.cshtml");
        }

        private SolicitudProducto CrearSolicitudProducto(SolicitudViewModel solicitud, int personaId) {
            
            var producto = new Producto
            {
                Estado = ProductoEstado.NoAprobado.Key,
                Nombre = solicitud.NombreProducto,
                Url = solicitud.UrlProducto,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            _context.Productos.Add(producto);
            _context.SaveChanges();

            var nuevaSolicitud = new SolicitudProducto
            {
                Estado = SolicitudProductoEstado.Pendiente.Key,
                PersonaId = personaId,
                UUID = Guid.NewGuid(),
                Producto = producto,
                ProductoId = producto.Id,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            _context.SolicitudProductos.Add(nuevaSolicitud);
            _context.SaveChanges();

            return nuevaSolicitud;

        }

        private int CrearPersona(SolicitudViewModel solicitud) {
            var nuevaPersona = new Persona
            {
                Nombre = solicitud.Nombre,
                TipoIdentificacion = solicitud.TipoIdentificacion,
                Correo = solicitud.Correo,
                Telefono = solicitud.Telefono,
                TipoPersona = solicitud.TipoPersona,
                NumeroIdentificacion = solicitud.NumeroIdentificacion
            };

            _context.Personas.Add(nuevaPersona);
            _context.SaveChanges();
            return nuevaPersona.Id;
        }
    }
}
