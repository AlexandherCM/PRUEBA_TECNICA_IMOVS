using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMOVS_WEB.Models;

namespace IMOVS_WEB.Services
{
    public class DetalleTicketService
    {
        private readonly Model1 _context = new Model1();

        public IEnumerable<DetalleTicket> GetAll() => _context.Detalles.ToList();

        public DetalleTicket GetById(int id) => _context.Detalles.Find(id);

        public void Create(DetalleTicket detalle)
        {
            _context.Detalles.Add(detalle);
            _context.SaveChanges();
        }

        public void Update(int id, DetalleTicket detalle)
        {
            var existing = _context.Detalles.Find(id);
            if (existing != null)
            {
                existing.ProductoId = detalle.ProductoId;
                existing.Cantidad = detalle.Cantidad;
                existing.PrecioUnitario = detalle.PrecioUnitario;
                existing.Total = detalle.Total;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var detalle = _context.Detalles.Find(id);
            if (detalle != null)
            {
                _context.Detalles.Remove(detalle);
                _context.SaveChanges();
            }
        }
    }
}