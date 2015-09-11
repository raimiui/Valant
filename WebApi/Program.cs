using System;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Autofac.Integration.WebApi;
using Valant.WebApi.Configs;

namespace Valant.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080")
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(AutofacConfig.Container)
            };

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
