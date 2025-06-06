namespace PRUEBA_TECNICA_IMOVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetalleCotizacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleCotizacion",
                c => new
                    {
                        IdDetalle = c.Int(nullable: false, identity: true),
                        IdCotizacion = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCotizacion = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdDetalle)
                .ForeignKey("dbo.Cotizaciones", t => t.IdCotizacion, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdCotizacion)
                .Index(t => t.IdProducto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleCotizacion", "IdProducto", "dbo.Productos");
            DropForeignKey("dbo.DetalleCotizacion", "IdCotizacion", "dbo.Cotizaciones");
            DropIndex("dbo.DetalleCotizacion", new[] { "IdProducto" });
            DropIndex("dbo.DetalleCotizacion", new[] { "IdCotizacion" });
            DropTable("dbo.DetalleCotizacion");
        }
    }
}
