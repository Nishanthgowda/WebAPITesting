using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace WebAPITesting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           //var JsonPFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
           // config.Formatters.Add(JsonPFormatter);

           config.EnableCors(new EnableCorsAttribute("*","*","*"));

             
        }
    }
}
