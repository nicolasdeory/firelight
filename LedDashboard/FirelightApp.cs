using Chromely;
using Chromely.Core;
using Chromely.Core.Host;
using LedDashboard.CustomHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LedDashboard
{
    class FirelightApp : ChromelyBasicApp
    {
        public override void Configure(IChromelyContainer container)
        {
            base.Configure(container);

            // Custom handlers
            container.RegisterSingleton(typeof(IChromelyNativeHost), typeof(IChromelyNativeHost).Name, typeof(CustomNativeWindow));
        }
    }
}
