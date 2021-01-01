using FirelightCommon;
using PipeMethodCalls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelightService
{
    /// <summary>
    /// Class for inter-process communication between backend service and UI. Keep in mind all methods will block.
    /// </summary>
    static class FrontendMessageService
    {

        static PipeServerWithCallback<IUIController, IBackendController> pipeServer;

        static bool connected = false;
        static bool pipelinerunning = false;

        public static async Task StartPipeline()
        {
            if (pipelinerunning)
                return;

            pipelinerunning = true;
            // The first type parameter is the controller interface that exposes methods to be called
            // by this client. The second one is the one that exposes methods to be called from another client.
            while (true)
            {
                try
                {
                    string pipeName = "firelightpipe-" + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    using (StreamWriter f = File.CreateText("pipename"))
                    {
                        f.Write(pipeName);
                    };

                    pipeServer = new PipeServerWithCallback<IUIController, IBackendController>(pipeName, () => new FirelightBackendController());

                    // pipeServer.SetLogger(message => Debug.WriteLine(message));
                    await pipeServer.WaitForConnectionAsync();
                    Debug.WriteLine("Pipeline connected - name " + pipeName);
                    connected = true;

                    // Delete the pipe name file
                    File.Delete("pipename");

                    await pipeServer.WaitForRemotePipeCloseAsync();
                    pipeServer.Dispose();
                    Debug.WriteLine("Pipeline disconnected.");
                    connected = false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message + " // " + e.StackTrace);

                }

            }

        }

        public static void SendError(string errId, string detailedErrTitle)
        {
            if (connected)
                _ = pipeServer.InvokeAsync(x => x.SendError(errId, detailedErrTitle));
        }
    }
}
