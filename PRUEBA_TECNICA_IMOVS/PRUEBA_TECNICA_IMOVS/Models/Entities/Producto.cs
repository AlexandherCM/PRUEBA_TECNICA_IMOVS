using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Required]
        public decimal PrecioVenta { get; set; } // Incluye IVA

        [Required]
        public int Stock { get; set; }

        public bool Activo { get; set; }

        [ForeignKey("TipoProducto")]
        public int TipoProductoId { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }

        public virtual ICollection<CotizacionDetalle> CotizacionDetalles { get; set; }
    }
}
