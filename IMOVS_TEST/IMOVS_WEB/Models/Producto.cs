using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMOVS_WEB.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }
        public virtual ICollection<DetalleTicket> Detalles { get; set; }
    }
}