using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightService
{
    interface IBackendController
    {
        public List<string> GetLights();
    }
}
