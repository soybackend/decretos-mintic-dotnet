using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tiposIdentificacion = WebApp.Models.TipoIdentificacion;
using tiposPersona = WebApp.Models.TipoPersona;

namespace WebApp.Models
{
    public class SolicitudViewModel
    {
        public string NumeroIdentificacion { set; get; }
        public string TipoIdentificacion { set; get; }
        public List<SelectListItem> TiposDeIdentificacion { get; } = new List<SelectListItem>
        {
        new SelectListItem { Value = tiposIdentificacion.CedulaCiudadania.Value, Text = "Cédula de Ciudadanía" },
        new SelectListItem { Value = tiposIdentificacion.CedulaExtranjeria.Value, Text = "Cédula de Extranjería" },
        new SelectListItem { Value = tiposIdentificacion.NIT.Value, Text = "NIT" },
        };
        public string TipoPersona { set; get; }
        public List<SelectListItem> TiposDePersona { get; } = new List<SelectListItem>
        {
        new SelectListItem { Value = tiposPersona.Natural.Value, Text = "Natural" },
        new SelectListItem { Value = tiposPersona.Juridica.Value, Text = "Jurídica" },
        };

        public string Nombre { set; get; }
        public string Correo { set; get; }

        public string Telefono { set; get; }

        [HiddenInput]
        public string TipoCertificado { get; set; } = "software";

        public bool VerificarCheck { get; set; }

        [HiddenInput]
        public string Producto { get; set; }

        public string NombreProducto { get; set; }
        public string UrlProducto { get; set; }

        public string NombreCurso { get; set; }
        public string UrlCurso { get; set; }
    }
}
