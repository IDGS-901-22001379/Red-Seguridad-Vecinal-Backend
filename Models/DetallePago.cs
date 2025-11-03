using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DetallePago
{
    [Key]
    public int DetalleID { get; set; }

    [Required]
    public int PagoID { get; set; }

    [Required]
    [StringLength(20)]
    public string TipoCargo { get; set; } = string.Empty; // "Mantenimiento" o "Servicio"

    [Required]
    public int CargoID { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoAplicado { get; set; }

    public DateTime FechaAplicacion { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("PagoID")]
    public virtual Pago Pago { get; set; } = null!;
}