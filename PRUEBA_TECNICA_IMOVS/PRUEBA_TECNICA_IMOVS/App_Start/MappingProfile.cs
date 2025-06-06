using AutoMapper;
using PRUEBA_TECNICA_IMOVS.DTOs;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.App_Start.Mappers 
{ 

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntidadEjemplo, EntidadEjemploReadDto>();

            CreateMap<EntidadEjemDTO, EntidadEjemplo>();
        }
    }
}