using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Invitado
{
    [Key]
    public int InvitadoID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreInvitado { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ApellidoPaternoInvitado { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ApellidoMaternoInvitado { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string CodigoQR { get; set; } = string.Empty;

    public DateTime FechaGeneracion { get; set; } = DateTime.Now;

    [Required]
    public DateTime FechaVencimiento { get; set; }

    public DateTime? FechaEntrada { get; set; }

    public DateTime? FechaSalida { get; set; }

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario Usuario { get; set; } = null!;
}