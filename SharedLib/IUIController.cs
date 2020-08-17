using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightCommon
{
    interface IUIController
    {
        void SendError(string err, string extraInfo);
    }
}
