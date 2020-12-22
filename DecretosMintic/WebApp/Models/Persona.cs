using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models;

public class Persona : AuditedModel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(2)]
    [Column("tipo_persona")]
    [Display(Name = "Tipo Persona")]
    public string TipoPersona { get; set; }

    [Required]
    [StringLength(100)]
    [Column("nombre")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [StringLength(3)]
    [Column("tipo_identificacion")]
    [Display(Name = "Tipo de Identificación")]
    public string TipoIdentificacion { get; set; }

    [StringLength(20)]
    [Column("numero_identificacion")]
    [Display(Name = "Número de identificación")]
    public string NumeroIdentificacion { get; set; }

    [StringLength(254)]
    [Column("correo")]
    [Display(Name = "Correo Electrónico")]
    public string Correo { get; set; }

    [StringLength(15)]
    [Column("telefono")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; }

    public ICollection<Certificado> Certificados { get; set; }
    public ICollection<SolicitudProducto> SolicitudesProducto { get; set; }
    public ICollection<SolicitudCurso> SolicitudesCurso { get; set; }
}
