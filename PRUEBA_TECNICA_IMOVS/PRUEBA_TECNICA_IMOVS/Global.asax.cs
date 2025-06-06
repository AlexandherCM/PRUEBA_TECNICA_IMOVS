using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http; 
using System.Web.Routing;
using AutoMapper;
using PRUEBA_TECNICA_IMOVS.App_Start.Mappers;
using PRUEBA_TECNICA_IMOVS.App_Start; 

namespace PRUEBA_TECNICA_IMOVS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
       
            GlobalConfiguration.Configure(WebApiConfig.Register);

            SwaggerConfig.Register();

            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

            UnityConfig.RegisterComponents();
        }
    }
}