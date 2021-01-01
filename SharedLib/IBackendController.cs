using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightCommon
{
    interface IBackendController
    {
        List<string[]> GetLights();

        Dictionary<string, string> GetSettings(string gameId);
        void UpdateSettings(string gameId, IDictionary<string, string> settings);
        void LogMessage(string message);
    }
}
