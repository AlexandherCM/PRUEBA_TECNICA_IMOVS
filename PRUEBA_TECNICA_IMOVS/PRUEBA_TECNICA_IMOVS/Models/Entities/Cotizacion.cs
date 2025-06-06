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
        [Key] public int IdCotizacion { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public int Total { get; set; }
        public bool Estado { get; set; }
    }
}