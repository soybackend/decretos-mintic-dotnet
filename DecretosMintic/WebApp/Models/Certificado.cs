using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApp.Models
{
    public class Certificado : AuditedModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("fecha", TypeName ="date")]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }


        [Column("uuid")]
        [Display(Name = "UUID")]
        public Guid UUID { get; set; }

        [Column("observaciones")]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        //Foreigns

        public Persona Persona { get; set; }

        [Column("persona_id")]
        public int PersonaId { get; set; }

        public Producto Producto { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }



    }
}
