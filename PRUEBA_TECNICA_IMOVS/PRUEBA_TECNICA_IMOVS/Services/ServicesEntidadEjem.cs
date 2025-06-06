using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PRUEBA_TECNICA_IMOVS.DTOs;
using PRUEBA_TECNICA_IMOVS.Interfaces; 
using PRUEBA_TECNICA_IMOVS.Interfaces.Services;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Services
{
    public class ServicesEntidadEjem : IServicesEntidadEjem
    {
        private readonly IEntidadEjem _entidadEjemRepository; 
        private readonly IMapper _mapper; 

        public ServicesEntidadEjem(IEntidadEjem entidadEjemRepository, IMapper mapper)
        {
            _entidadEjemRepository = entidadEjemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntidadEjemploReadDto>> GetAllEntidadesAsync()
        {
            var entidades = _entidadEjemRepository.GetAll();
            return await Task.FromResult(_mapper.Map<IEnumerable<EntidadEjemploReadDto>>(entidades));
        }

        public async Task<EntidadEjemploReadDto> GetEntidadByIdAsync(int id)
        {
            var entidad = await _entidadEjemRepository.GetByIdAsync(id);
            if (entidad == null)
            {
                return null;
            }
            return _mapper.Map<EntidadEjemploReadDto>(entidad);
        }

        public async Task<EntidadEjemploReadDto> CreateEntidadAsync(EntidadEjemDTO entidadDto)
        {
            var entidad = _mapper.Map<EntidadEjemplo>(entidadDto);

            _entidadEjemRepository.Add(entidad);
            var saved = await _entidadEjemRepository.SaveChangesAsync();

            if (!saved)
            {
                return null;
            }

            return _mapper.Map<EntidadEjemploReadDto>(entidad);
        }

        public async Task<bool> UpdateEntidadAsync(int id, EntidadEjemDTO entidadDto)
        {
            var entidadExistente = await _entidadEjemRepository.GetByIdAsync(id);
            if (entidadExistente == null)
            {
                return false;
            }

            _mapper.Map(entidadDto, entidadExistente); 

            _entidadEjemRepository.Update(entidadExistente);
            return await _entidadEjemRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteEntidadAsync(int id)
        {
            var entidad = await _entidadEjemRepository.GetByIdAsync(id);
            if (entidad == null)
            {
                return false;
            }

            _entidadEjemRepository.Delete(entidad);
            return await _entidadEjemRepository.SaveChangesAsync();
        }
    }
}