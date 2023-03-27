using Chromely;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Chromely.Core.Network;

namespace FirelightUI
{
    class FirelightApp : ChromelyBasicApp
    {
        private static List<Type> GetControllers<T>()
            where T : Attribute
        {
            // TODO: This is a generally useful function that uses reflection, must be abstracted elsewhere
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttributes(typeof(T), true).Length > 0).ToList();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            //List<Type> controllers = GetControllers<AppControllerAttribute>();

            //foreach (Type t in controllers)
            //{
            //    services.AddSingleton(typeof(ChromelyController), t);
            //}
            RegisterControllerAssembly(services, typeof(FirelightApp).Assembly);
        }
    }
}
