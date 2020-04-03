using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard
{
    public enum TaskRunnerLogLevel { Normal = 0, Verbose = 1 }
    public static class TaskRunner
    {
        const TaskRunnerLogLevel LOG_LEVEL = TaskRunnerLogLevel.Normal;
        /*public static void Run(Action action)
        {
            Task.Run(() =>
            {
                try
                {
                    action.Invoke();
                } catch (Exception e)
                {
                    if (LOG_LEVEL < TaskRunnerLogLevel.Verbose)
                    {
                        if (e is WebException || e is TaskCanceledException) return;
                    }
                    Console.WriteLine("Exception ocurred in task: " + e);
                    Console.WriteLine(e.Message);
                    if (e.InnerException != null) Console.WriteLine("Inner: " + e.InnerException);
                    Console.WriteLine(e.StackTrace);
                }
            });
        }*/

        public static async Task RunAsync(Task t)
        {
            // This helps with debugging because it catches the stack trace.
            try
            {
                await t;
            } catch (Exception e)
            {
                if (LOG_LEVEL < TaskRunnerLogLevel.Verbose)
                {
                    if (e is WebException || e is TaskCanceledException) return;
                }
                Console.WriteLine("Exception ocurred in task: " + e);
                Console.WriteLine(e.Message);
                if (e.InnerException != null) Console.WriteLine("Inner: " + e.InnerException);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
