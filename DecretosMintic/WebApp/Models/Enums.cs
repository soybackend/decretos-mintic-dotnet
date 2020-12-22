using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductoEstado {
        private ProductoEstado(string key, string value) { 
            Value = value;
            Key = key;
        }
        public string Value { get; set; }
        public string Key { get; set; }

        public static ProductoEstado Aprobado { get { return new ProductoEstado("A", "Aprobado"); } }
        public static ProductoEstado NoAprobado { get { return new ProductoEstado("N", "No Aprobado"); } }

        public static string TraerNombreEstado(string acronimo)
        {
            switch (acronimo)
            {
                case "A":
                    return "Aprobado";
                case "N":
                    return "No Aprobado";
                default:
                    return "No Aprobado";
            }
        }
    }

    public class TipoPersona
    {
        private TipoPersona(string key, string value) {
            Value = value;
            Key = key;
        }
        public string Value { get; set; }
        public string Key { get; set; }
        public static TipoPersona Natural { get { return new TipoPersona("Natural","N"); } }
        public static TipoPersona Juridica { get { return new TipoPersona("Jurídica", "J"); } }
    }

    public class TipoIdentificacion
    {
        private TipoIdentificacion(string value) { Value = value; }
        public string Value { get; set; }

        public static TipoIdentificacion CedulaCiudadania { get { return new TipoIdentificacion("CC"); } }
        public static TipoIdentificacion CedulaExtranjeria { get { return new TipoIdentificacion("CE"); } }
        public static TipoIdentificacion NIT { get { return new TipoIdentificacion("NIT"); } }

        /// <summary>
        /// Retorna el tipo de identificación completo
        /// </summary>
        /// <returns></returns>
        public static string TraerNombreTipoIdentificacion(string acronimo) {
            switch (acronimo) {
                case "CC":
                    return "Cédula de Ciudadanía";
                case "CE":
                    return "Cédula de Extranjería";
                case "NIT":
                    return "NIT";
                default:
                    return "Cédula de Ciudadanía";
            }
        }


    }

    public class CursoEstado
    {
        private CursoEstado(string key, string value) { 
            Value = value;
            Key = key;
        }
        public string Value { get; set; }
        public string Key { get; set; }
        public static CursoEstado Aprobado { get { return new CursoEstado("A", "Aprobado"); } }
        public static CursoEstado NoAprobado { get { return new CursoEstado("N", "No Aprobado"); } }

        public static string TraerNombreEstado(string acronimo)
        {
            switch (acronimo)
            {
                case "A":
                    return "Aprobado";
                case "N":
                    return "No Aprobado";
                default:
                    return "No Aprobado";
            }
        }
    }

    public class SolicitudProductoEstado
    {
        private SolicitudProductoEstado(string key, string value) { Value = value; Key = key; }
        public string Value { get; set; }

        public string Key { get; set; }

        public static SolicitudProductoEstado Pendiente { get { return new SolicitudProductoEstado("P", "Pendiente"); } }
        public static SolicitudProductoEstado Aprobado { get { return new SolicitudProductoEstado("A", "Aprobada"); } }

        public static SolicitudProductoEstado Negado { get { return new SolicitudProductoEstado("N", "Negada"); } }


        public static string TraerNombreTipo(string acronimo)
        {
            switch (acronimo)
            {
                case "P":
                    return "Pendiente";
                case "A":
                    return "Aprobada";
                default:
                    return "Negada";
            }
        }
    }

    public class TipoCertificado
    {
        private TipoCertificado(string value) { Value = value; }
        public string Value { get; set; }

        public static TipoCertificado Software { get { return new TipoCertificado("software"); } }
        public static TipoCertificado Curso { get { return new TipoCertificado("curso"); } }
    }
}
