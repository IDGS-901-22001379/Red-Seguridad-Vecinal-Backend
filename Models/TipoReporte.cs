using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TipoReporte
{
    [Key]
    public int TipoReporteID { get; set; }

    [Required]
    [StringLength(15)]
    public string Nombre { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}