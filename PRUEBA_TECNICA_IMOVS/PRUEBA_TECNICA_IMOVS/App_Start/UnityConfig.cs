// En App_Start/UnityConfig.cs

using System.Web.Http;
using Unity;
using Unity.WebApi; // ESTE ES EL USING CRÍTICO para la integración de Web API

// Ya NO NECESITAS este using:
// using Unity.Lifetime; // ¡QUITA ESTA LÍNEA!

using PRUEBA_TECNICA_IMOVS.Models; 
using PRUEBA_TECNICA_IMOVS.Interfaces; 
using PRUEBA_TECNICA_IMOVS.Repositories; 
using PRUEBA_TECNICA_IMOVS.Interfaces.Services;
using PRUEBA_TECNICA_IMOVS.Services; 
using AutoMapper; 
using PRUEBA_TECNICA_IMOVS.App_Start.Mappers; 

namespace PRUEBA_TECNICA_IMOVS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<Context>(); 

            container.RegisterType<IEntidadEjem, EntidadEjemploRepository>();

            container.RegisterType<IServicesEntidadEjem, ServicesEntidadEjem>();


            container.RegisterInstance<IMapper>(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}