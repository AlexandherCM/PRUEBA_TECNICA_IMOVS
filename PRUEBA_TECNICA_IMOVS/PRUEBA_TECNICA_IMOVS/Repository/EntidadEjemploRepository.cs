using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PRUEBA_TECNICA_IMOVS.Interfaces;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Repositories
{
    public class EntidadEjemploRepository : IEntidadEjem 
    {
        private readonly Context _context; 

        public EntidadEjemploRepository(Context context) 
        {
            _context = context;
        }

        public IQueryable<EntidadEjemplo> GetAll()
        {
            return _context.EntidadesEjemplo;
        }

        public async Task<EntidadEjemplo> GetByIdAsync(int id)
        {
            return await _context.EntidadesEjemplo.FindAsync(id);
        }

        public void Add(EntidadEjemplo entity)
        {
            _context.EntidadesEjemplo.Add(entity);
        }

        public void Update(EntidadEjemplo entity)
        {
            _context.EntidadesEjemplo.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(EntidadEjemplo entity)
        {
            _context.EntidadesEjemplo.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public bool Exists(int id)
        {
            return _context.EntidadesEjemplo.Count(e => e.ID == id) > 0;
        }
    }
}