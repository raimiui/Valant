using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using Valant.Repositories;
using Valant.Repositories.Interfaces;
using Valant.Services;
using Valant.Services.Interfaces;

namespace Valant.WebApi.Configs
{
    public class AutofacConfig
    {
        static volatile IContainer _container;
        static readonly object syncObj = new object();

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                    lock (syncObj)
                        if (_container == null)
                            _container = RegisterTypes();

                return _container;
            }
        }

        public static IContainer RegisterTypes()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<InventoryService>().As<IInventoryService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();

            // Repositories
            builder.RegisterType<InventoryRepository>().As<IInventoryRepository>();

            // RegisterApiControllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            return container;
        }
    }
}
