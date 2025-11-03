using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TipoAmenidad
{
    [Key]
    public int TipoAmenidadID { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public TimeSpan HorarioInicio { get; set; }

    [Required]
    public TimeSpan HorarioFin { get; set; }

    public bool Activo { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Amenidad> Amenidades { get; set; } = new List<Amenidad>();
}