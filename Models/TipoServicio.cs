using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TipoServicio
{
    [Key]
    public int TipoServicioID { get; set; }

    [Required]
    [StringLength(15)]
    public string Nombre { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    // Navigation properties
    public virtual ICollection<ServiciosCatalogo> ServiciosCatalogo { get; set; } = new List<ServiciosCatalogo>();
    public virtual ICollection<SolicitudesServicio> SolicitudesServicio { get; set; } = new List<SolicitudesServicio>();
}