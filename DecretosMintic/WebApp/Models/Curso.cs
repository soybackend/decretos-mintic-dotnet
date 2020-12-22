using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Curso : AuditedModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(200)]
        [Column("url")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [StringLength(1)]
        [Column("estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        public ICollection<SolicitudCurso> SolicitudesCurso { get; set; }
    }
}
