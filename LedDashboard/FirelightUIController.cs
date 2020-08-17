using FirelightCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FirelightUI
{
    class FirelightUIController : IUIController
    {
        public void SendError(string err, string detailedTitle)
        {
            GlobalAppState.AddError(err, detailedTitle);
        }
    }
}
