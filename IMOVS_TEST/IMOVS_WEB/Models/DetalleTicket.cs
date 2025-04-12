using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace IMOVS_WEB.Models
{
    public class DetalleTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleTicketId { get; set; }

        [Required]
        [ForeignKey(nameof(Ticket))]
        public int TicketId { get; set; }

        [Required]
        [ForeignKey(nameof(Producto))]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal PrecioUnitario { get; set; }

        [NotMapped]
        public decimal PrecioTotal => Cantidad * PrecioUnitario;

        public virtual Ticket Ticket { get; set; }
        public virtual Producto Producto { get; set; }
    }
}