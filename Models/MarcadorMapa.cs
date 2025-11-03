using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MarcadorMapa
{
    [Key]
    public int MarcadorID { get; set; }

    public int? UsuarioID { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,8)")]
    public decimal Latitud { get; set; }

    [Required]
    [Column(TypeName = "decimal(11,8)")]
    public decimal Longitud { get; set; }

    [Required]
    [StringLength(15)]
    public string Indicador { get; set; } = string.Empty; // Peligroso, Alerta, Mantenimiento

    [StringLength(500)]
    public string? Comentario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario? Usuario { get; set; }
}