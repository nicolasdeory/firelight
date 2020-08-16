using FirelightCommon;
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

            List<string[]> res = null;
            res = await pipeClient.InvokeAsync(x => x.GetLights());
            return res;

        }

        public static void LogMessage(string message)
        {
            _ = pipeClient.InvokeAsync(x => x.LogMessage(message));
        }
    }
}
