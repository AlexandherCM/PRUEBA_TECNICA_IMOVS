using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PRUEBA_TECNICA_IMOVS.Models.Entities
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public bool Estatus { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal PrecioUnitario { get; set; }

        // Precio con IVA incluido. Ej. 116.00 si IVA = 16% y el precio base era 100.

        // Navegación hacia DetalleCotizacion
        public virtual ICollection<DetalleCotizacion> Detalles { get; set; }

        public Producto()
        {
            Detalles = new HashSet<DetalleCotizacion>();
        }
    }
}
