using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.ViewModels;
using Wkhtmltopdf.NetCore;

namespace WebApp.Utils
{
    public class GeneradorPDF
    {
        readonly IGeneratePdf _generadorPDF;
        private GeneradorQR _generadorQR;

        private string _host;


        public GeneradorPDF(IGeneratePdf generadorQR, string host)
        {
            _generadorPDF = generadorQR;
            _generadorQR = new GeneradorQR();
            _host = host;
        }

        public System.IO.MemoryStream Generar(Persona persona, Certificado certificado) {

            var headerImage = _host + "/Assets/logos.png";


            var datos = new PDFModel(persona,certificado);

            var generatedQR = _generadorQR.Generar("http://beneficiostributarios.mintic.gov.co/certificado/"+certificado.UUID.ToString());
            var qrPath = _host + "/Assets/" + generatedQR.FileName;

            string formHtml = "<!DOCTYPE html>" +
                                    "<html>" +
                                    "<body>" +
                                        "<div style='margin: auto; width: 100%; text-align: center;'><img src='" + headerImage + "' height='120'/></div>" +
                                        "<div style = 'margin-top: 80px; display: block; float: left;'>" +
                                            "<p style='font: 18px Helvetica, sans-serif;' > Bogotá D.C, " + datos.Fecha + " </p>" +
                                        "</div>" +
                                        "<div style='margin-top: 120px; width: 100%; display: block; float: left;'>" +
                                            "<p style='font: 18px Helvetica, sans-serif;'>El Ministerio de Tecnologías de la Información y las Comunicaciones certifica que el producto <b>" + datos.NombreProducto + "</b> cumple con los criterios establecidos en el Decreto 1412 del 25 de agosto de 2017." +
                                        "</p></div>" +
                                        "<div style='margin-top: 20px; width: 100%; display: block; float: left;'>" +
                                            "<p style='font: 18px Helvetica, sans-serif;'>El presente certificado de expide a los " + datos.Dia + " días del mes " + datos.Mes + " del año " + datos.Anio + " por solicitud de " + datos.NombreSolicitante + " identificado(a) con " + datos.TipoIdentificacion + " No. " + datos.NumeroIdentificacion + "." +
                                        "</p></div>" +
                                        "<div style='margin-top: 40px; width: 100%; display: block; float: right; text-align: right;'><img src='" + qrPath + "' height='240' /></div>" +
                                        "<div style=' margin-top: 330px; display: block; float: left ; width:100%; text-align:center; '>" +
                                            "<p style='font: 14px Helvetica, sans-serif;'> Ministerio de Tecnologías de la Información y las Comunicaciones </p>" +
                                            "<p style='font: 14px Helvetica, sans-serif;'> Edificio Murillo Toro Cra. 8a entre calles 2 y 13, Bogotá, Colombia - Código Postal 111711 </p>" +
                                            "<p style='font: 14px Helvetica, sans-serif;'> Teléfono Conmutador: +57(1) 344 34 60 - Línea Gratuita: 01-800-0914014</p>" +
                                        "</div>" +
                                    "</body>" +
                                    "</html>";

            var pdf = _generadorPDF.GetPDF(formHtml);
            var pdfStream = new System.IO.MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;

            _generadorQR.BorrarArchivo(generatedQR.FullPath);

            return pdfStream;
        }
    }
}
