using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SolicitudesServicio
{
    [Key]
    public int SolicitudID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required]
    public int TipoServicioID { get; set; }

    public int? PersonaAsignado { get; set; }

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Urgencia { get; set; } = string.Empty;

    public DateOnly? FechaPreferida { get; set; }

    public TimeSpan? HoraPreferida { get; set; }

    [StringLength(15)]
    public string Estado { get; set; } = "Pendiente";

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public DateTime? FechaAsignacion { get; set; }

    public DateTime? FechaCompletado { get; set; }

    [StringLength(500)]
    public string? NotasAdmin { get; set; }

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("TipoServicioID")]
    public virtual TipoServicio TipoServicio { get; set; } = null!;

    [ForeignKey("PersonaAsignado")]
    public virtual ServiciosCatalogo? ServicioAsignado { get; set; }

    public virtual ICollection<CargoServicio> CargosServicios { get; set; } = new List<CargoServicio>();
}