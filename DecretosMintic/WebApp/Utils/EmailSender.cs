using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Utils
{
    public class EmailSender
    {
        private readonly string _smtp = "smtp.outlook.office365.com";
        private readonly int _port = 587;
        private readonly string _user = "beneficiostributarios@mintic.gov.co";
        private readonly string _password = "Mintic2018!";

        public EmailSender() { 
            
        }


        public void EnviarNotifProducto(SolicitudProducto solicitud, Persona persona) {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("MinTic", _user));
            message.To.Add(new MailboxAddress("Señor(a) "+persona.Nombre, persona.Correo));
            message.Subject = "Solicitud de Exclusión de IVA";

            message.Body = new TextPart("plain")
            {
                Text = String.Format("La solicitud de exclusión de iva del producto {0} se encuentra en estado {1} </br> {2}",
                    solicitud.Producto.Nombre,
                    SolicitudProductoEstado.TraerNombreTipo(solicitud.Estado),
                    "<a href='#'>Puedes descargar una certificación de este producto en este enlace.</a>"                )
            };

    
            using (var client = new SmtpClient())
            {
                client.Connect(_smtp, _port, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_user, _password);

                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
