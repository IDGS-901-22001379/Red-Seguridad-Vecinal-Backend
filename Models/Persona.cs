using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Persona
{
    [Key]
    public int PersonaID { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ApellidoPaterno { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ApellidoMaterno { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Navigation properties
    public virtual Usuario? Usuario { get; set; }
    public virtual PersonalMantenimiento? PersonalMantenimiento { get; set; }
}