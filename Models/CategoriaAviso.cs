using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CategoriaAviso
{
    [Key]
    public int CategoriaID { get; set; }

    [Required]
    [StringLength(15)]
    public string Nombre { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Aviso> Avisos { get; set; } = new List<Aviso>();
}