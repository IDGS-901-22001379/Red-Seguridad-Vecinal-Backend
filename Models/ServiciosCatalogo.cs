using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ServiciosCatalogo
{
    [Key]
    public int ServicioID { get; set; }

    [Required]
    public int TipoServicioID { get; set; }

    [Required]
    [StringLength(200)]
    public string NombreEncargado { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Email { get; set; }

    public int NumeroServiciosCompletados { get; set; } = 0;

    public bool Disponible { get; set; } = true;

    [StringLength(500)]
    public string? NotasInternas { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;

    // Navigation properties
    [ForeignKey("TipoServicioID")]
    public virtual TipoServicio TipoServicio { get; set; } = null!;

    public virtual ICollection<SolicitudesServicio> SolicitudesServicio { get; set; } = new List<SolicitudesServicio>();
}