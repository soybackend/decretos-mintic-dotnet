using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Utils
{
    public class SolicitudCourse
    {

        private ReportesContext _contexto;

        public SolicitudCourse(ReportesContext contexto)
        {
            _contexto = contexto;
        }

        public SolicitudCurso Solicitar(SolicitudViewModel solicitud, int personaId) {
            
            var curso = new Curso { 
                Nombre = solicitud.NombreCurso,
                Url = solicitud.UrlCurso,
                Estado = CursoEstado.NoAprobado.Key,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            _contexto.Add(curso);
            _contexto.SaveChanges();

            var solicitudCurso = new SolicitudCurso { 
               CursoId = curso.Id,
               Curso = curso,
               PersonaId = personaId,
               Estado = SolicitudProductoEstado.Pendiente.Key,
               Created = DateTime.Now,
               Updated = DateTime.Now,
               UUID = Guid.NewGuid()
            };

            _contexto.Add(solicitudCurso);
            _contexto.SaveChanges();

            return solicitudCurso;
        }
    }
}
