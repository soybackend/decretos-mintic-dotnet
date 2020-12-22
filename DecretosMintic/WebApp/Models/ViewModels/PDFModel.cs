using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Models.ViewModels
{
    /// <summary>
    /// Representa el modelo con el que se renderiza la certifiación pdf
    /// Contiene información acerca de producto, fecha y persona.
    /// </summary>
    public class PDFModel
    {

        public string Fecha { get; set; }
        public string NombreProducto { get; set; }
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }
        public string NombreSolicitante{ get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }

        public PDFModel(Persona persona, Certificado certificado) {
            var now = DateTime.Now;
            Fecha = now.ToString("dd-MM-yyyy");
            NombreProducto = certificado.Producto.Nombre;
            Dia = now.Day.ToString();
            Mes = now.Month.ToString();
            Anio = now.Year.ToString();
            NombreSolicitante = persona.Nombre;
            TipoIdentificacion = WebApp.Models.TipoIdentificacion.TraerNombreTipoIdentificacion(
                persona.TipoIdentificacion);
            NumeroIdentificacion = persona.NumeroIdentificacion;
        }
    }
}
