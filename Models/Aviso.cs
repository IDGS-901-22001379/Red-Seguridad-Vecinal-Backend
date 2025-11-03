using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Aviso
{
    [Key]
    public int AvisoID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    [Required]
    public int CategoriaID { get; set; }

    [Required]
    [StringLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    public DateTime? FechaEvento { get; set; }

    public DateTime FechaPublicacion { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("CategoriaID")]
    public virtual CategoriaAviso Categoria { get; set; } = null!;
}