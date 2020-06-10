using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace LedDashboardCore
{
    public enum TaskRunnerLogLevel { Normal = 0, Verbose = 1 }
    public static class TaskUtils
    {
        const TaskRunnerLogLevel LOG_LEVEL = TaskRunnerLogLevel.Normal;

        public static Task CatchExceptions(this Task t)
        {
            return t.ContinueWith((t) =>
            {
                Exception e = t.Exception;
                Debug.WriteLine("Exception ocurred in task: " + e);
                Debug.WriteLine(e.Message);
                if (e.InnerException != null) Debug.WriteLine("Inner: " + e.InnerException);
                Debug.WriteLine(e.StackTrace);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static async Task RunAsync(Task t) // unused, doesn't really work
        {
            // This helps with debugging because it catches the stack trace.
            try
            {
                await t;
            }
            catch (Exception e)
            {
                if (LOG_LEVEL < TaskRunnerLogLevel.Verbose)
                {
                    if (e is WebException || e is TaskCanceledException) return;
                }
                Debug.WriteLine("Exception ocurred in task: " + e);
                Debug.WriteLine(e.Message);
                if (e.InnerException != null) Debug.WriteLine("Inner: " + e.InnerException);
                Debug.WriteLine(e.StackTrace);
            }
        }
    }
}
