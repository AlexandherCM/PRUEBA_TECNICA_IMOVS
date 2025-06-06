using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("DetalleCotizacion")]
    public class DetalleCotizacion
    {
        [Key] public int IdDetalle { get; set; }

        [ForeignKey("Cotizacion")]
        public int IdCotizacion { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal TotalCotizacion { get; set; }

        // Navegación
        public virtual Cotizacion Cotizacion { get; set; }
        public virtual Producto Producto { get; set; }
    }
}