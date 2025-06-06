using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("CotizacionDetalle")]
    public class CotizacionDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Cotizacion")]
        public int CotizacionId { get; set; }
        public virtual Cotizacion Cotizacion { get; set; }

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; } // Copiado del producto al momento de cotizar

        [NotMapped]
        public decimal PrecioTotal => PrecioUnitario * Cantidad;
    }
}
