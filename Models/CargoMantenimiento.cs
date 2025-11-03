using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CargoMantenimiento
{
    [Key]
    public int CargoMantenimientoID { get; set; }

    public int? UsuarioID { get; set; }

    public int? PersonalMantenimientoID { get; set; }

    [Required]
    [StringLength(200)]
    public string Concepto { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Monto { get; set; }

    [Required]
    public DateOnly FechaVencimiento { get; set; }

    [StringLength(15)]
    public string Estado { get; set; } = "Pendiente";

    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoPagado { get; set; } = 0.00m;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal SaldoPendiente { get; private set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("UsuarioID")]
    public virtual Usuario? Usuario { get; set; }

    [ForeignKey("PersonalMantenimientoID")]
    public virtual PersonalMantenimiento? PersonalMantenimiento { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}