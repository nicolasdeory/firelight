using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirelightService
{
    class Program
    {
        static void Main(string[] args)
        {
            //Process.
            //Console.WriteLine("Hello World!");
            //Process.Start("LedDashboard.exe");

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Icon = new System.Drawing.Icon("./icon.ico");
            trayIcon.Visible = true;
            trayIcon.Text = "Firelight";
            trayIcon.BalloonTipTitle = "????????????????????????";
            trayIcon.BalloonTipText = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            trayIcon.Click += TrayIcon_Click;
            // icon.ShowBalloonTip(2000);


            ContextMenuStrip menuStrip = new ContextMenuStrip();
            ToolStripDropDownButton testDropdownButton = new ToolStripDropDownButton("Open Firelight", null, null, "Open Firelight");
            ToolStripButton btn = new ToolStripButton("asdsad");
            // menuStrip.Items.Add(btn);
            menuStrip.Items.Add("Show Firelight", null, ShowFirelightClicked);
            menuStrip.Items.Add("Exit Firelight", null, ExitFirelightClicked);
            trayIcon.ContextMenuStrip = menuStrip;


            //trayIcon.
            Application.Run();
            /*while (true)
            {
                Debug.WriteLine("Hello");
                Thread.Sleep(1000);
            }*/
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
            Process[] processes = Process.GetProcessesByName("Firelight");
            if (processes.Length > 0)
                return;
            Process p = Process.Start("Firelight.exe");
            ChildProcessTracker.AddProcess(p);
        }
    }
}
