using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Controllers
{
    public class ProductoController : ApiController
    {
        private Context db = new Context();

        // GET: api/Producto
        public IHttpActionResult GetProductos()
        {
            var productos = db.Productos
                .Where(p => p.Estatus == true && p.Stock > 0)
                .ToList();

            return Ok(productos);
        }

        // GET: api/Producto/5
        public IHttpActionResult GetProducto(int id)
        {
            var producto = db.Productos.Find(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // POST: api/Producto
        public IHttpActionResult PostProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Productos.Add(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        // PUT: api/Producto/5
        public IHttpActionResult PutProducto(int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = db.Productos.Find(id);
            if (existente == null)
                return NotFound();

            existente.Nombre = producto.Nombre;
            existente.Tipo = producto.Tipo;
            existente.Stock = producto.Stock;
            existente.Estatus = producto.Estatus;
            existente.PrecioUnitario = producto.PrecioUnitario;

            db.SaveChanges();
            return Ok(existente);
        }

        // DELETE: api/Producto/5
        public IHttpActionResult DeleteProducto(int id)
        {
            var producto = db.Productos.Find(id);
            if (producto == null)
                return NotFound();

            db.Productos.Remove(producto);
            db.SaveChanges();

            return Ok();
        }
    }
}
