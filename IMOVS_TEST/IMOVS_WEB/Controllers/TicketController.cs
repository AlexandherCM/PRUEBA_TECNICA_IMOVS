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
    public class TicketController : ApiController
    {
        private readonly TicketService _service = new TicketService();

        public IHttpActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            var ticket = _service.GetById(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        public IHttpActionResult Post(Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Create(ticket);
            return Ok(ticket);
        }

        public IHttpActionResult Put(int id, Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _service.GetById(id);
            if (existing == null)
                return NotFound();

            _service.Update(id, ticket);
            return Ok(ticket);
        }

        public IHttpActionResult Delete(int id)
        {
            var ticket = _service.GetById(id);
            if (ticket == null)
                return NotFound();

            _service.Delete(id);
            return Ok();
        }
    }
}