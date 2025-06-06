namespace PRUEBA_TECNICA_IMOVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cotizaciones",
                c => new
                    {
                        IdCotizacion = c.Int(nullable: false, identity: true),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Iva = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCotizacion);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        NombreProducto = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Estatus = c.Boolean(nullable: false),
                        TipoProducto = c.String(),
                    })
                .PrimaryKey(t => t.IdProducto);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Productos");
            DropTable("dbo.Cotizaciones");
        }
    }
}
