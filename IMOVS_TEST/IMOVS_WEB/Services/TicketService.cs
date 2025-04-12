using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMOVS_WEB.Models;

namespace IMOVS_WEB.Services
{
    public class TicketService
    {
        private readonly Model1 _context = new Model1();

        public IEnumerable<Ticket> GetAll() => _context.Tickets.ToList();

        public Ticket GetById(int id) => _context.Tickets.Find(id);

        public void Create(Ticket ticket)
        {
            ticket.FechaCreacion = System.DateTime.Now;
            ticket.Estatus = "Por pagar";
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public void Update(int id, Ticket ticket)
        {
            var existing = _context.Tickets.Find(id);
            if (existing != null)
            {
                existing.Folio = ticket.Folio;
                existing.FechaLiquidacion = ticket.FechaLiquidacion;
                existing.Estatus = ticket.Estatus;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
            }
        }
    }
}