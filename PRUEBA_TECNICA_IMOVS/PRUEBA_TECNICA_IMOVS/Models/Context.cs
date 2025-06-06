using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using PRUEBA_TECNICA_IMOVS.Models.Entities; // 👈 Asegúrate de importar esto

namespace PRUEBA_TECNICA_IMOVS.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public DbSet<TipoProducto> TiposProducto { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<CotizacionDetalle> CotizacionDetalles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Si luego necesitas configurar relaciones o tablas con Fluent API, lo haces aquí
        }
    }
}
