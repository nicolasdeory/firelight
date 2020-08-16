using Chromely.Core.Logging;
using FirelightUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FirelightUI
{
    class FirelightLogger : TraceListener
    {
        public override void Write(string message)
        {
            BackendMessageService.LogMessage(message);
        }

        public override void WriteLine(string message)
        {
            BackendMessageService.LogMessage(message);
        }
    }
}
