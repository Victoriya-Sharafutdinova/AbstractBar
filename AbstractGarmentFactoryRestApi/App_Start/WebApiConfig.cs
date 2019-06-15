using AbstractGarmentFactoryMVC;
using AbstractGarmentFactoryView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AbstractGarmentFactoryRestApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            APICustomer.Connect();
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}
