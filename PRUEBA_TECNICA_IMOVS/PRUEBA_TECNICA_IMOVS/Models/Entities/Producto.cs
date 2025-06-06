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
        [StringLength(250)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Tipo { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El Stock no puede ser negativo.")]
        public int Stock { get; set; }

        public bool Estatus { get; set; }

        public virtual ICollection<DetallesCotizacion> DetallesCotizacion { get; set; }
    }
}