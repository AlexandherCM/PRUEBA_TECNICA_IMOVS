namespace PRUEBA_TECNICA_IMOVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CotizacionDetalle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CotizacionId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cotizacion", t => t.CotizacionId, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.CotizacionId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Cotizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Estado = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        PrecioVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                        TipoProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoProducto", t => t.TipoProductoId, cascadeDelete: true)
                .Index(t => t.TipoProductoId);
            
            CreateTable(
                "dbo.TipoProducto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CotizacionDetalle", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.Producto", "TipoProductoId", "dbo.TipoProducto");
            DropForeignKey("dbo.CotizacionDetalle", "CotizacionId", "dbo.Cotizacion");
            DropIndex("dbo.Producto", new[] { "TipoProductoId" });
            DropIndex("dbo.CotizacionDetalle", new[] { "ProductoId" });
            DropIndex("dbo.CotizacionDetalle", new[] { "CotizacionId" });
            DropTable("dbo.TipoProducto");
            DropTable("dbo.Producto");
            DropTable("dbo.Cotizacion");
            DropTable("dbo.CotizacionDetalle");
        }
    }
}
