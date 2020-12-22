using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class SolicitudCurso : AuditedModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [StringLength(1)]
        [Column("estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Column("uuid")]
        [Display(Name = "UUID")]
        public Guid UUID { get; set; }

        [Column("observaciones")]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        // Foreigns

        public Persona Persona { get; set; }

        [Column("persona_id")]
        public int PersonaId { get; set; }

        public Curso Curso { get; set; }

        [Column("curso_id")]
        public int CursoId { get; set; }
    }
}
