using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PersonalMantenimiento
{
    [Key]
    public int PersonalMantenimientoID { get; set; }

    [Required]
    public int PersonaID { get; set; }

    [Required]
    [StringLength(50)]
    public string Puesto { get; set; } = string.Empty;

    [Required]
    public DateOnly FechaContratacion { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Sueldo { get; set; }

    [StringLength(50)]
    public string? TipoContrato { get; set; }

    [StringLength(20)]
    public string? Turno { get; set; }

    [StringLength(100)]
    public string? DiasLaborales { get; set; }

    public bool Activo { get; set; } = true;

    [StringLength(500)]
    public string? Notas { get; set; }

    // Navigation properties
    [ForeignKey("PersonaID")]
    public virtual Persona Persona { get; set; } = null!;

    public virtual ICollection<CargoMantenimiento> CargosMantenimiento { get; set; } = new List<CargoMantenimiento>();
}