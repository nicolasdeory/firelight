using Chromely.Core;
using Chromely.Core.Configuration;
using LedDashboardCore;
using System;
using System.Windows.Forms;

namespace LedDashboard
{
    class LedDashboard
    {
        [STAThread]
        static void Main(string[] args)
        {
            var config = DefaultConfiguration.CreateForRuntimePlatform();
            config.WindowOptions.Title = "Firelight";
            config.WindowOptions.RelativePathToIconFile = "app/assets/icon.ico";
            config.StartUrl = "local://app/lights.html";
            AppBuilder
            .Create()
            .UseApp<FirelightApp>()
            .UseConfiguration<IChromelyConfiguration>(config)
            .Build()
            .Run(args);
        }
    }
}
