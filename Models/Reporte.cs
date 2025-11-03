using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reporte
{
    [Key]
    public int ReporteID { get; set; }

    public int? UsuarioID { get; set; }

    [Required]
    public int TipoReporteID { get; set; }

    [StringLength(100)]
    public string? Titulo { get; set; }

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,8)")]
    public decimal Latitud { get; set; }

    [Required]
    [Column(TypeName = "decimal(11,8)")]
    public decimal Longitud { get; set; }

    [StringLength(500)]
    public string? DireccionTexto { get; set; }

    public bool EsAnonimo { get; set; } = false;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public bool Visto { get; set; } = false;

    public string? Imagen { get; set; }

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario? Usuario { get; set; }

    [ForeignKey("TipoReporteID")]
    public virtual TipoReporte TipoReporte { get; set; } = null!;
}