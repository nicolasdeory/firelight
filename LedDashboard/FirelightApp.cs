using Chromely;
using Chromely.Core;
using Chromely.Core.Host;
using Chromely.Core.Network;
using FirelightUI.CustomHandlers;
using FirelightUI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightUI
{
    class FirelightApp : ChromelyBasicApp
    {
        public override void Configure(IChromelyContainer container)
        {
            base.Configure(container);

            // Controllers
            container.RegisterSingleton(typeof(ChromelyController), Guid.NewGuid().ToString(), typeof(LightController));

            // Custom handlers
            container.RegisterSingleton(typeof(IChromelyNativeHost), typeof(IChromelyNativeHost).Name, typeof(CustomNativeWindow));

        }
    }
}
