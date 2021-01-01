using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightService
{
    interface IBackendController
    {
        public List<string[]> GetLights();

        Dictionary<string, string> GetSettings(string gameId);
        void UpdateSettings(string gameId, IDictionary<string, string> settings);
        public void LogMessage(string message);
    }
}
