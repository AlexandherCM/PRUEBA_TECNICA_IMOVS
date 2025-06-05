using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("DetalleCotizaciones")]
    public class DetalleCotizacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleCotizacionId { get; set; }

        // FK a Cotizacion
        [Required]
        public int CotizacionId { get; set; }

        [ForeignKey("CotizacionId")]
        public virtual Cotizacion Cotizacion { get; set; }

        // FK a Producto
        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto Producto { get; set; }

        [Required]
        public int Unidades { get; set; }
        // Cantidad de unidades cotizadas para este producto.

        [Required]
        public decimal PrecioUnitario { get; set; }
        // Copia del precio del producto al momento de la cotización (incluye IVA).

        [Required]
        public decimal Subtotal { get; set; }
        // = PrecioUnitario * Unidades (ya trae IVA).
    }
}
