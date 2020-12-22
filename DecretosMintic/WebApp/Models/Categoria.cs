using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Categoria: AuditedModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        // [Display(Name = "First Name")]
        public string Nombre { get; set; }

        public ICollection<ProductoCategorias> ProductosCategorias { get; set; }
    }
}
