using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Models
{
    public partial class Context : DbContext
    {
        public Context():base("name=Context")
        {}
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<DetalleCotizacion> DetalleCotizaciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

    }
}
