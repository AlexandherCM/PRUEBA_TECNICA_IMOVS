//using IMOVS_TEST.DbModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IMOVS_WEB.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<DetalleTicket> Detalles { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<DetalleTicket>().ToTable("DetalleTicket");
            modelBuilder.Entity<Pago>().ToTable("Pago");
        }
    }
}
