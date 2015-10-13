using Owin;
using System.Web.Http;

namespace ServerSTUNLibrary
{

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            appBuilder
                .UseWebApi(config)
                .UseNancy();
        }
    }
}
