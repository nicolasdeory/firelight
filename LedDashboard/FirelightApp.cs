using Chromely;
using Chromely.Core;
using Chromely.Core.Host;
using Chromely.Core.Network;
using FirelightUI.CustomHandlers;
using FirelightUI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Chromely.Core.Helpers;
using Chromely.CefGlue.Browser.EventParams;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace FirelightUI
{
    class FirelightApp : ChromelyEventedApp
    {
        private static List<Type> GetControllers<T>()
            where T : Attribute
        {
            // TODO: This is a generally useful function that uses reflection, must be abstracted elsewhere
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttributes(typeof(T), true).Length > 0).ToList();
        }

        public override void Configure(IChromelyContainer container)
        {
            base.Configure(container);

            // Controllers

            List<Type> controllers = GetControllers<AppControllerAttribute>();

            foreach (Type t in controllers)
            {
                container.RegisterSingleton(typeof(ChromelyController), Guid.NewGuid().ToString(), t);
            }

            //container.RegisterSingleton(typeof(ChromelyController), Guid.NewGuid().ToString(), typeof(GlobalController));

            // Custom handlers
            container.RegisterSingleton(typeof(IChromelyNativeHost), typeof(IChromelyNativeHost).Name, typeof(CustomNativeWindow));

        }

        protected override void OnBeforeClose(object sender, BeforeCloseEventArgs eventArgs)
        {
            BackendMessageService.Disconnect();
        }
    }
}
