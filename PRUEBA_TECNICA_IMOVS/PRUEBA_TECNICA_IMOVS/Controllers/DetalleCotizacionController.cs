using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    public class CotizacionController : ApiController
    {
        private Context db = new Context();

        // GET: api/Cotizacion
        public IHttpActionResult GetCotizaciones()
        {
            var cotizaciones = db.Cotizaciones
                .OrderByDescending(c => c.Fecha)
                .ToList();
            return Ok(cotizaciones);
        }

        // GET: api/Cotizacion/5
        public IHttpActionResult GetCotizacion(int id)
        {
            var cotizacion = db.Cotizaciones.Find(id);
            if (cotizacion == null) return NotFound();
            return Ok(cotizacion);
        }

        // POST: api/Cotizacion
        public IHttpActionResult PostCotizacion([FromBody] Cotizacion cotizacion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            cotizacion.EstadoVenta = false; // por defecto
            cotizacion.Fecha = System.DateTime.Now;

            db.Cotizaciones.Add(cotizacion);
            db.SaveChanges();
            return Ok(cotizacion);
        }

        // PUT: api/Cotizacion/5
        public IHttpActionResult ConfirmarVenta(int id)
        {
            var cotizacion = db.Cotizaciones
                .Include("Detalles.Producto")
                .FirstOrDefault(c => c.CotizacionId == id);

            if (cotizacion == null || cotizacion.EstadoVenta)
                return BadRequest("Cotización no encontrada o ya confirmada.");

            foreach (var detalle in cotizacion.Detalles)
            {
                var producto = db.Productos.Find(detalle.ProductoId);
                if (producto.Stock >= detalle.Unidades)
                    producto.Stock -= detalle.Unidades;
                else
                    return BadRequest($"No hay suficiente stock de: {producto.Nombre}");
            }

            cotizacion.EstadoVenta = true;
            db.SaveChanges();
            return Ok(cotizacion);
        }

        // DELETE: api/Cotizacion/5
        public IHttpActionResult DeleteCotizacion(int id)
        {
            var cotizacion = db.Cotizaciones.Find(id);
            if (cotizacion == null) return NotFound();

            db.Cotizaciones.Remove(cotizacion);
            db.SaveChanges();
            return Ok();
        }
    }
}
