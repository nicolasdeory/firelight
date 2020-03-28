using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    public class ProcessListenerService
    {
        static Dictionary<string, Action<string>> listenedProcesses = new Dictionary<string, Action<string>>();
        public static void Init()
        {
            Task.Run(async () =>
            {
                for (;;)
                {
                    
                    foreach(var process in listenedProcesses.Keys)
                    {
                        Process[] pname = Process.GetProcessesByName(process);
                        if (pname.Length == 0) continue;
                        listenedProcesses[process].Invoke(process);
                    }
                    await Task.Delay(2000);
                }
                
            });
        }

        public static void Register(string name, Action<string> processOpenCallback)
        {
            listenedProcesses.Add(name, processOpenCallback);
        }
    }
}
