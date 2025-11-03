using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TipoUsuario
{
    [Key]
    public int TipoUsuarioID { get; set; }

    [Required]
    [StringLength(15)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}