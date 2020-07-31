using FirelightCommon;
using PipeMethodCalls;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightUI
{
    /// <summary>
    /// Class for inter-process communication between backend service and UI. Keep in mind all methods will block.
    /// </summary>
    static class BackendMessageService
    {

        static PipeClientWithCallback<IBackendController, IUIController> pipeClient;

        public static bool IsInitialized { get { return pipeClient != null; } }
        /// <summary>
        /// Establishes a connection to the backend. Blocks until client is connected.
        /// </summary>
        public static void InitConnection()
        {
            pipeClient = new PipeClientWithCallback<IBackendController, IUIController>("firelightpipe", () => new FirelightUIController());
            pipeClient.ConnectAsync().Wait(2000);
        }

        /// <summary>
        /// Gets the light data.
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetLights()
        {
            if (!IsInitialized)
                throw new InvalidOperationException("Pipe not initialized yet. Call InitConnection() first");

            return pipeClient.InvokeAsync(x => x.GetLights()).Result;

        }
    }
}
