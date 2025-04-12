using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMOVS_WEB.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        [Required, StringLength(50)]
        public string Folio { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaLiquidacion { get; set; }

        [Required, StringLength(20)]
        public string Estatus { get; set; }

        public virtual ICollection<DetalleTicket> Detalles { get; set; }
            = new List<DetalleTicket>();

        public virtual ICollection<Pago> Pagos { get; set; }
            = new List<Pago>();
    }
}