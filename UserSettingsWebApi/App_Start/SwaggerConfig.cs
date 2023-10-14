using System.Web.Http;
using WebActivatorEx;
using UserSettingsWebApi;
using Swashbuckle.Application;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace UserSettingsWebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c => c.SingleApiVersion("v1", "ErrorHandlingWebAPI"))
              .EnableSwaggerUi();
        }
    }
}
