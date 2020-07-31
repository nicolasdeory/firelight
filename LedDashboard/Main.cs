using Chromely.Core;
using Chromely.Core.Configuration;
using System;
using System.Diagnostics;

namespace FirelightService
{
    class LedDashboard
    {
        [STAThread]
        static void Main(string[] args)
        {

            Process[] processes = Process.GetProcessesByName("FirelightService");
            if (processes.Length == 0)
            {
                Debug.WriteLine("Service not running, starting");
                Process.Start("FirelightService.exe", "ui");
                return;
            }

            // Chromely configuration
            var config = DefaultConfiguration.CreateForRuntimePlatform();
            config.WindowOptions.Title = "Firelight";
            config.WindowOptions.RelativePathToIconFile = "app/assets/icon.ico";
            //config.WindowOptions.WindowFrameless = true;
            config.StartUrl = "local://app/lights.html";
#if RELEASE
            config.DebuggingMode = false;
#endif
            config.WindowOptions.Size = new WindowSize(1200, 700);
            config.WindowOptions.StartCentered = true;

            // Chromely initialization
            AppBuilder
            .Create()
            .UseApp<FirelightApp>()
            .UseConfiguration<IChromelyConfiguration>(config)
            .Build()
            .Run(args);
        }
    }
}
