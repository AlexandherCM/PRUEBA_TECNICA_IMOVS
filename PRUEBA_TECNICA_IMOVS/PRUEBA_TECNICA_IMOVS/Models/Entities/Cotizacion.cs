using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("Cotizaciones")]
    public class Cotizacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CotizacionId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public decimal Total { get; set; }  // decimal sin atributo Column

        [Required]
        public bool EstadoVenta { get; set; }

        public virtual ICollection<DetalleCotizacion> Detalles { get; set; }

        public Cotizacion()
        {
            Fecha = DateTime.Now;
            EstadoVenta = false;
            Detalles = new HashSet<DetalleCotizacion>();
        }
    }
}
