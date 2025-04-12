using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace IMOVS_TEST.DbModels
{
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PagoId { get; set; }

        [Required, StringLength(50)]
        public string Folio { get; set; }

        [Required]
        [ForeignKey(nameof(Ticket))]
        public int TicketId { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        public int NumeroPago { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Monto { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}