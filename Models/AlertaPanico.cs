using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AlertaPanico
{
    [Key]
    public int AlertaID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,8)")]
    public decimal Latitud { get; set; }

    [Required]
    [Column(TypeName = "decimal(11,8)")]
    public decimal Longitud { get; set; }

    public DateTime FechaHora { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario Usuario { get; set; } = null!;
}