using LedDashboardCore;
using System;
using System.Windows.Forms;

namespace LedDashboard
{
    class LedDashboard
    {
        [STAThread]
        static void Main(string[] args)
        {
            KeyboardHook.Init();
            Application.EnableVisualStyles();
            Application.Run(new MainUI());
        }
    }
}
