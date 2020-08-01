using PipeMethodCalls;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelightService
{
    class FirelightService
    {
        static PipeServerWithCallback<IUIController, IBackendController> pipeServer;
        static void Main(string[] args)
        {

            Process[] processes = Process.GetProcessesByName("FirelightService");
            if (processes.Length > 1)
            {
                Debug.WriteLine("Service already running!");
                return;
            }
            if (args.Length > 0 && args[0] == "ui")
            {
                // Use -ui argument to open the UI directly
                OpenFirelightUI();
            }

            LightManager.Init();
            RenderTrayIcon();

            Application.Run();
        }

        private static async Task StartPipeline()
        {
            // The first type parameter is the controller interface that exposes methods to be called
            // by this client. The second one is the one that exposes methods to be called from another client.
            while (true)
            {
                try
                {
                    pipeServer = new PipeServerWithCallback<IUIController, IBackendController>("firelightpipe", () => new FirelightBackendController());
                    // TODO: Reconnections are still a bit unstable. Pipes must be closed and recreated properly. As of now, client can only reconnect once.
                    // Subsequent reconnections won't work

                    // pipeServer.SetLogger(message => Debug.WriteLine(message));
                    await pipeServer.WaitForConnectionAsync();
                    Debug.WriteLine("Pipeline connected");
                    await pipeServer.WaitForRemotePipeCloseAsync();
                    pipeServer.Dispose();
                    Debug.WriteLine("Pipeline disconnected. Waiting for reconnection...");
                } catch (Exception e)
                {
                    Debug.WriteLine(e.Message + " // " + e.StackTrace);

                }
                
            }

        }

        private static void RenderTrayIcon()
        {
            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Icon = new System.Drawing.Icon("./icon.ico");
            trayIcon.Visible = true;
            trayIcon.Text = "Firelight";
            trayIcon.Click += TrayIcon_Click;

            ContextMenuStrip menuStrip = new ContextMenuStrip();
            menuStrip.Items.Add("Show Firelight", null, ShowFirelightClicked);
            menuStrip.Items.Add("Exit Firelight", null, ExitFirelightClicked);
            trayIcon.ContextMenuStrip = menuStrip;
        }

        private static void ShowFirelightClicked(object sender, EventArgs e)
        {
            OpenFirelightUI();
        }

        private static void ExitFirelightClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void TrayIcon_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = (MouseEventArgs)e;
            if (mouseArgs.Button != MouseButtons.Left)
                return;
            OpenFirelightUI();

        }

        private static void OpenFirelightUI()
        {
            _ = StartPipeline();
            Process[] processes = Process.GetProcessesByName("Firelight");
            if (processes.Length > 0)
            {
                IntPtr handle = processes[0].MainWindowHandle;
                ShowWindow(handle, 9);
                SetForegroundWindow(handle);
            }
            else
            {
                Process p = Process.Start("Firelight.exe");
                ChildProcessTracker.AddProcess(p);
            }           
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
