using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class QRPersonal
{
    [Key]
    public int QRID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required]
    [StringLength(500)]
    public string CodigoQR { get; set; } = string.Empty;

    public DateTime FechaGeneracion { get; set; } = DateTime.Now;

    [Required]
    public DateTime FechaVencimiento { get; set; }

    public bool Activo { get; set; } = true;

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario Usuario { get; set; } = null!;
}