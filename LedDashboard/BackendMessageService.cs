using FirelightCommon;
using FirelightUI.ControllerModel;
using PipeMethodCalls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelightUI
{
    /// <summary>
    /// Class for inter-process communication between backend service and UI. Keep in mind all methods will block.
    /// </summary>
    static class BackendMessageService
    {

        static PipeClientWithCallback<IBackendController, IUIController> pipeClient;

        public static bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Establishes a connection to the backend. Blocks until client is connected.
        /// </summary>
        public static async Task InitConnection()
        {
            string pipeName = File.ReadAllText("pipename");
            pipeClient = new PipeClientWithCallback<IBackendController, IUIController>(pipeName, () => new FirelightUIController());
            //pipeClient.SetLogger(message => Debug.WriteLine(message));
            await pipeClient.ConnectAsync();
            Debug.WriteLine("Pipe connected");
            await Task.Delay(1200);
            IsInitialized = true;
        }

        public static void Disconnect()
        {
            pipeClient.Dispose();
        }

        /// <summary>
        /// Gets the light data.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<string[]>> GetLights()
        {
            if (!IsInitialized)
                //throw new InvalidOperationException("Pipe not initialized yet. Call InitConnection() first");
                return new List<string[]>();

            return await Task.Run(() => 
            {
                Task<List<string[]>> task = pipeClient.InvokeAsync(x => x.GetLights());
                bool completedInTime = task.Wait(2000);
                if (!completedInTime)
                {
                    Debug.WriteLine("Connection to main process timed out");
                    // MessageBox.Show("Sorry, something went wrong. Please open the app again.");
                    Application.Exit();
                    //Environment.Exit(0);
                }
                return task.Result;
            });
        }

        /// <summary>
        /// Gets settings from a certain game
        /// </summary>
        public static async Task<Dictionary<string, string>> GetSettings(Game g)
        {
            if (!IsInitialized)
                //throw new InvalidOperationException("Pipe not initialized yet. Call InitConnection() first");
                return null;

            return await Task.Run(() =>
            {
                Task<Dictionary<string, string>> task = pipeClient.InvokeAsync(x => x.GetSettings(g.Id));
                bool completedInTime = task.Wait(2000);
                if (!completedInTime)
                {
                    Debug.WriteLine("Connection to main process timed out");
                    Application.Restart();
                    Environment.Exit(0);
                }
                return task.Result;
            });
        }

        /// <summary>
        /// Updates settings from a certain game
        /// </summary>
        public static void UpdateSettings(Game g, IDictionary<string, string> settings)
        {
            if (IsInitialized)
                _ = pipeClient.InvokeAsync(x => x.UpdateSettings(g.Id, settings));
        }

        public static void LogMessage(string message)
        {
            if (IsInitialized)
                _ = pipeClient.InvokeAsync(x => x.LogMessage(message));
        }
    }
}
