using System.Linq;
using System.Threading.Tasks;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Interfaces 
{
    public interface IEntidadEjem
    {
        IQueryable<EntidadEjemplo> GetAll();
        Task<EntidadEjemplo> GetByIdAsync(int id);
        void Add(EntidadEjemplo entity);
        void Update(EntidadEjemplo entity);
        void Delete(EntidadEjemplo entity);
        Task<bool> SaveChangesAsync();
        bool Exists(int id);
    }
}