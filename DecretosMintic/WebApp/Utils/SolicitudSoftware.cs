using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Utils
{
    public class SolicitudSoftware
    {
        private ReportesContext _contexto;

        public SolicitudSoftware(ReportesContext context) {
            _contexto = context;
        }

        public Certificado Solicitar(SolicitudViewModel solicitud, int personaId) {

            //Se solicita verificar un producto que existe
            if (!solicitud.VerificarCheck) {
                if (solicitud.Producto != "" && personaId != 0)
                {
                    //Si existe la persona y el producto, se expide el certificado
                    var certificado = new Certificado();
                    certificado.PersonaId = personaId;
                    certificado.ProductoId = int.Parse(solicitud.Producto);
                    certificado.Fecha = DateTime.Now;
                    certificado.UUID = Guid.NewGuid();

                    _contexto.Add(certificado);
                    _contexto.SaveChanges();

                    var producto = _contexto.Productos.Find(certificado.ProductoId);
                    certificado.Producto = producto;

                    return certificado;
                }
                else
                {
                    //La persona o el producto no existen 
                    return new Certificado();
                }
            }

            return new Certificado();

        }
    }
}
