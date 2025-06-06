using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    public class CotizacionesController : ApiController
    {
        private readonly Context db = new Context();

        // GET api/cotizaciones
        public async Task<IEnumerable<Cotizacion>> Get()
        {
            return await db.Cotizaciones.Include(c => c.Detalles).ToListAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostCotizacion(Cotizacion cotizacion)
        {
            if (cotizacion == null || cotizacion.Detalles == null || !cotizacion.Detalles.Any())
                return BadRequest("Cotización inválida");

            cotizacion.Fecha = DateTime.Now;
            cotizacion.Estado = "Pendiente"; // Estado por defecto

            // Asignamos PrecioUnitario desde producto por seguridad
            foreach (var detalle in cotizacion.Detalles)
            {
                var producto = await db.Productos.FindAsync(detalle.ProductoId);
                if (producto == null || !producto.Activo || producto.Stock <= 0)
                    return BadRequest($"Producto no válido o sin stock: ID {detalle.ProductoId}");

                detalle.PrecioUnitario = producto.PrecioVenta;
            }

            db.Cotizaciones.Add(cotizacion);
            await db.SaveChangesAsync();

            return Ok(new { mensaje = "Cotización guardada correctamente", cotizacion.Id });
        }

    }
}
