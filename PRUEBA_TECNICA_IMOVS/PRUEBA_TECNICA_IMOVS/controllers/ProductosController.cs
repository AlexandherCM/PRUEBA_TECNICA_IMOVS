using PRUEBA_TECNICA_IMOVS.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly Context db = new Context();

        // GET api/productos
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetProductosActivos()
        {
            var productos = await db.Productos
                .Where(p => p.Activo && p.Stock > 0)
                .Select(p => new {
                    p.Id,
                    p.Nombre,
                    p.PrecioVenta
                })
                .ToListAsync();

            return Ok(productos);
        }
    }
}
