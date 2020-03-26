using NAudio.Wave;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
