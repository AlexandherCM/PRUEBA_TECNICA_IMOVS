using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("DetallesCotizacion")]
    public class DetallesCotizacion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        public virtual Producto Producto { get; set; }

        [Required]
        public int CotizacionId { get; set; }

        public virtual Cotizacion Cotizacion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La Cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public decimal Subtotal { get; set; }
    }
}