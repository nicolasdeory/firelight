using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace FirelightCore
{
    public class ProcessListenerService
    {
        public delegate void ProcessChangedHandler(string name, int pid);
        public static event ProcessChangedHandler ProcessInFocusChanged;

        static List<string> listenedProcesses = new List<string>();

        static string currentOpenedProcess = "";

        static CancellationTokenSource cancelToken;

        public static void Start() // TODO: Handle two registered processes running at the same time
        {
            cancelToken?.Cancel();
            cancelToken = new CancellationTokenSource();
            Task.Run(async () =>
            {
                bool processChangedToARegisteredOne = false;
                bool atLeastARegisteredProcessIsRunning = false;
                while (true)
                {
                    if (cancelToken.IsCancellationRequested)
                        return;
                    processChangedToARegisteredOne = false;
                    atLeastARegisteredProcessIsRunning = false;
                    foreach (var process in listenedProcesses)
                    {
                        //Process[] prss = Process.GetProcesses();
                        Process[] pname = Process.GetProcessesByName(process); // TODO: Sometimes not firing on first boot?
                        IntPtr handleInFocus = GetForegroundWindow();
                        int processInFocus;
                        GetWindowThreadProcessId(handleInFocus, out processInFocus);
                        if (pname.Length == 0 || pname[0].Id != processInFocus) continue;
                        if (process != currentOpenedProcess)
                        {
                            currentOpenedProcess = process;
                            ProcessInFocusChanged?.Invoke(process, pname[0].Id);
                            processChangedToARegisteredOne = true;
                            break;
                        }
                        atLeastARegisteredProcessIsRunning = true;
                    }
                    if (!processChangedToARegisteredOne)
                    {
                        if (!atLeastARegisteredProcessIsRunning)
                        {
                            ProcessInFocusChanged?.Invoke("", -1); // no process is running
                            currentOpenedProcess = "";
                        }
                        await Task.Delay(2000);
                    }

                }

            }).CatchExceptions(true);
        }


        public static void Stop()
        {
            cancelToken?.Cancel();
            currentOpenedProcess = "";
        }

        public static void Restart()
        {
            currentOpenedProcess = "";
        }

        public static void Register(string name)
        {
            listenedProcesses.Add(name);
        }

        public static int GetProcessId(string name)
        {
            Process[] pname = Process.GetProcessesByName(name);
            if (pname.Length == 0)
                throw new InvalidOperationException($"No processes with name ${name} are running");
            return pname[0].Id;

        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
    }
}
