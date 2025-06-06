using System.Collections.Generic;
using System.Threading.Tasks;
using PRUEBA_TECNICA_IMOVS.DTOs;

namespace PRUEBA_TECNICA_IMOVS.Interfaces.Services
{
    public interface IServicesEntidadEjem
    {
        Task<IEnumerable<EntidadEjemploReadDto>> GetAllEntidadesAsync();
        Task<EntidadEjemploReadDto> GetEntidadByIdAsync(int id);
        Task<EntidadEjemploReadDto> CreateEntidadAsync(EntidadEjemDTO entidadDto);
        Task<bool> UpdateEntidadAsync(int id, EntidadEjemDTO entidadDto);
        Task<bool> DeleteEntidadAsync(int id);
    }
}