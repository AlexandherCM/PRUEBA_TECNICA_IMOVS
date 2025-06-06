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
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public bool Confirmada { get; set; }

        public decimal TotalSinIVA { get; set; }
        
        [Required]
        public decimal IVA { get; set; }
        
        public decimal TotalConIVA { get; set; }

        public virtual ICollection<DetallesCotizacion> Detalles { get; set; }

        public Cotizacion()
        {
            Detalles = new HashSet<DetallesCotizacion>();
        }
    }
}