using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Amenidad
{
    [Key]
    public int AmenidadID { get; set; }

    [Required]
    public int TipoAmenidadID { get; set; }

    [Required]
    [StringLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Ubicacion { get; set; }

    public int? Capacidad { get; set; }

    public bool Activo { get; set; } = true;

    // Navigation properties
    [ForeignKey("TipoAmenidadID")]
    public virtual TipoAmenidad TipoAmenidad { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}