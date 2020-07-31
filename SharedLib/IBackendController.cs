using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightCommon
{
    interface IBackendController
    {
        public List<string[]> GetLights();
    }
}
