using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightCommon
{
    interface IBackendController
    {
        List<string[]> GetLights();

        void LogMessage(string message);
    }
}
