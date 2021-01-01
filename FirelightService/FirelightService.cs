using PipeMethodCalls;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelightService
{
    class FirelightService
    {
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
            _ = FrontendMessageService.StartPipeline();
            Process[] processes = Process.GetProcessesByName("FirelightUI");
            if (processes.Length > 0)
            {
                IntPtr handle = processes[0].MainWindowHandle;
                ShowWindow(handle, 9);
                SetForegroundWindow(handle);
            }
            else
            {
                Process p = Process.Start("FirelightUI.exe");
                ChildProcessTracker.AddProcess(p);
            }
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
