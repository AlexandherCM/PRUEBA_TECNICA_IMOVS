using IMOVS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMOVS_WEB.Services
{
    public class ProductoService
    {
        private readonly Model1 _context;

        public ProductoService()
        {
            _context = new Model1();
        }

        public IEnumerable<Producto> GetAll() => _context.Productos.ToList();

        public Producto GetById(int id) => _context.Productos.Find(id);

        public void Create(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        public void Update(int id, Producto producto)
        {
            var existing = _context.Productos.Find(id);
            if (existing != null)
            {
                existing.Nombre = producto.Nombre;
                existing.PrecioUnitario = producto.PrecioUnitario;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
        }
    }
}