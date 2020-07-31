using Chromely.Core;
using Chromely.Core.Configuration;
using System;

namespace LedDashboard
{
    class LedDashboard
    {
        [STAThread]
        static void Main(string[] args)
        {
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
            // config.WindowOptions.DisableResizing = true;
            //  config.WindowOptions.FramelessOption = FramelessOption

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
