using System;
using System.Windows.Forms;

namespace LedDashboard
{
    class LedDashboard
    {
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.Run(new MainUI());
        }
    }
}
