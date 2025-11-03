using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago
{
    [Key]
    public int PagoID { get; set; }

    [Required]
    public int UsuarioID { get; set; }

    public int? CargoMantenimientoID { get; set; }

    [Required]
    [StringLength(50)]
    public string FolioUnico { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoTotal { get; set; }

    [Required]
    [StringLength(20)]
    public string TipoPago { get; set; } = string.Empty;

    public DateTime FechaPago { get; set; } = DateTime.Now;

    [StringLength(10)]
    public string MetodoPago { get; set; } = "Debito";

    [StringLength(4)]
    public string? UltimosDigitosTarjeta { get; set; }

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("CargoMantenimientoID")]
    public virtual CargoMantenimiento? CargoMantenimiento { get; set; }

    public virtual ICollection<DetallePago> DetallesPago { get; set; } = new List<DetallePago>();
}