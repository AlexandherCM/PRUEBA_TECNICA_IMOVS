using IMOVS_WEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using IMOVS_WEB.Models;

namespace IMOVS_WEB.Controllers
{
    public class DetalleTicketController : ApiController
    {
        private readonly DetalleTicketService _service = new DetalleTicketService();

        public IHttpActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            var detalle = _service.GetById(id);
            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        public IHttpActionResult Post(DetalleTicket detalle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Create(detalle);
            return Ok(detalle);
        }

        public IHttpActionResult Put(int id, DetalleTicket detalle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _service.GetById(id);
            if (existing == null)
                return NotFound();

            _service.Update(id, detalle);
            return Ok(detalle);
        }

        public IHttpActionResult Delete(int id)
        {
            var detalle = _service.GetById(id);
            if (detalle == null)
                return NotFound();

            _service.Delete(id);
            return Ok();
        }
    }
}