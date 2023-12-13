using System.Web.Http;
using WebAPITesting;
using Swashbuckle.Application;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebAPITesting
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WebAPITesting");                        
                    })
                .EnableSwaggerUi();
        }
    }
}
