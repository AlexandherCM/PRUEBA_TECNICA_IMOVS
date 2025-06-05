using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    public class DetalleCotizacionController : ApiController
    {
        private Context db = new Context();

        // POST: api/DetalleCotizacion
        public IHttpActionResult PostDetalle([FromBody] DetalleCotizacion detalle)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Calcular Subtotal
            detalle.Subtotal = detalle.PrecioUnitario * detalle.Unidades;

            db.DetalleCotizaciones.Add(detalle);
            db.SaveChanges();
            return Ok(detalle);
        }

        // DELETE: api/DetalleCotizacion/5
        public IHttpActionResult DeleteDetalle(int id)
        {
            var detalle = db.DetalleCotizaciones.Find(id);
            if (detalle == null) return NotFound();

            db.DetalleCotizaciones.Remove(detalle);
            db.SaveChanges();
            return Ok();
        }
    }
}
