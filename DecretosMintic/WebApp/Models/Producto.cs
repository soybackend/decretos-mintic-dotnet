using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class ProductoCategorias
    {
        [Column("categoria_id")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
    public class Producto : AuditedModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(200)]
        [Column("url")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [StringLength(1)]
        [Column("estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        public ICollection<ProductoCategorias> ProductosCategorias { get; set; }

        public ICollection<Certificado> Certificados { get; set; }

        public ICollection<SolicitudProducto> SolicitudesProducto { get; set; }


    }
}




//class Producto(AuditedModel):
//    estado_choices = (
//        ('A', 'Aprobado'),
//        ('N', 'No Aprobado'),
//    )
//    nombre = models.CharField(max_length = 100)
//    categorias = models.ManyToManyField(Categoria)
//    url = models.CharField(max_length = 200, null = True, blank = True)
//    estado = models.CharField(
//        max_length = 1,
//        choices = estado_choices,
//        default = 'A'
//    )

//    def __str__(self):
//        return self.nombre