namespace PRUEBA_TECNICA_IMOVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cotizaciones",
                c => new
                    {
                        CotizacionId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstadoVenta = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CotizacionId);
            
            CreateTable(
                "dbo.DetalleCotizaciones",
                c => new
                    {
                        DetalleCotizacionId = c.Int(nullable: false, identity: true),
                        CotizacionId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Unidades = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DetalleCotizacionId)
                .ForeignKey("dbo.Cotizaciones", t => t.CotizacionId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.CotizacionId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        Stock = c.Int(nullable: false),
                        Estatus = c.Boolean(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleCotizaciones", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.DetalleCotizaciones", "CotizacionId", "dbo.Cotizaciones");
            DropIndex("dbo.DetalleCotizaciones", new[] { "ProductoId" });
            DropIndex("dbo.DetalleCotizaciones", new[] { "CotizacionId" });
            DropTable("dbo.Productos");
            DropTable("dbo.DetalleCotizaciones");
            DropTable("dbo.Cotizaciones");
        }
    }
}
