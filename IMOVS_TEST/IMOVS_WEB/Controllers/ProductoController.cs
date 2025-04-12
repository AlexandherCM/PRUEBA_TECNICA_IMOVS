using IMOVS_WEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using IMOVS_WEB.Models;
using IMOVS_WEB.Services;
using System.Web.Http.Routing;


namespace IMOVS_WEB.Controllers
{

        public class ProductoController : ApiController
        {
            private readonly ProductoService _service = new ProductoService();

            public IHttpActionResult Get()
            {
                var productos = _service.GetAll();
                return Ok(productos);
            }

            public IHttpActionResult Get(int id)
            {
                var producto = _service.GetById(id);
                if (producto == null)
                    return NotFound();

                return Ok(producto);
            }

            public IHttpActionResult Post(Producto producto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _service.Create(producto);
                return Ok(producto);
            }

            public IHttpActionResult Put(int id, Producto producto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existing = _service.GetById(id);
                if (existing == null)
                    return NotFound();

                _service.Update(id, producto);
                return Ok(producto);
            }

            public IHttpActionResult Delete(int id)
            {
                var producto = _service.GetById(id);
                if (producto == null)
                    return NotFound();

                _service.Delete(id);
                return Ok();
            }
        }
    }