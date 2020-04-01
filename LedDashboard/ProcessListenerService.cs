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
        public delegate void ProcessChangedHandler(string name);
        public static event ProcessChangedHandler ProcessInFocusChanged;

        static List<string> listenedProcesses = new List<string>();

        static string currentOpenedProcess = "";

        public static void Start()
        {
            Task.Run(async () =>
            {
                bool processChangedToARegisteredOne = false;
                bool atLeastARegisteredProcessIsRunning = false;
                for (;;)
                {
                    processChangedToARegisteredOne = false;
                    atLeastARegisteredProcessIsRunning = false;
                    foreach (var process in listenedProcesses)
                    {
                        Process[] pname = Process.GetProcessesByName(process); // TODO: Sometimes not firing on first boot?
                        if (pname.Length == 0) continue;
                        if (process != currentOpenedProcess)
                        {
                            currentOpenedProcess = process;
                            ProcessInFocusChanged?.Invoke(process);
                            processChangedToARegisteredOne = true;
                            break;
                        }
                        atLeastARegisteredProcessIsRunning = true;
                    }
                    if(!processChangedToARegisteredOne)
                    {
                        if (!atLeastARegisteredProcessIsRunning)
                        {
                            ProcessInFocusChanged?.Invoke(""); // no process is running
                            currentOpenedProcess = "";
                        }
                        await Task.Delay(2000);
                    }
                    
                }
                
            });
        }

        public static void Restart()
        {
            currentOpenedProcess = "";
        }

        public static void Register(string name)
        {
            listenedProcesses.Add(name);
        }
    }
}
