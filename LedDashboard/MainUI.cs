using Numpy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using Python.Runtime;

namespace LedDashboard
{

    // This is a debug UI window that displays the LED strip.

    class MainUI : Form
    {
        PictureBox box;
        Graphics canvas;
        LedManager ledManager;
        FormsPlot plt;

        public MainUI()
        {
            this.Size = new Size(1600, 500);
            box = new PictureBox();
            box.Size = new Size(1600, 50);
            box.Location = new Point(0, 10);
            box.BackColor = Color.White;
            canvas = box.CreateGraphics();

            plt = new FormsPlot();
            plt.Location = new Point(0, 100);
            plt.Size = new Size(800, 200);

            this.Controls.AddRange(new Control[] { box, plt });

            Gradient.GeneratePalettes();

            ledManager = new LedManager(170, true);
            ledManager.UpdateDisplay += UpdateUI;

        }

        bool updatingUI = false;
        /// <summary>
        /// Update the virtual LED strip with the given LED color data.
        /// </summary>
        public void UpdateUI(Led[] leds)
        {
            if(!updatingUI)
            {
                updatingUI = true;
                int i = 0;

                foreach (var led in leds)
                {
                    byte[] col = led.color.ToRGB();
                    SolidBrush colBrush = new SolidBrush(Color.FromArgb(col[0], col[1], col[2]));
                    canvas.FillRectangle(colBrush, new Rectangle(0 + i * 10, 0, 10, 10));
                    i++;
                }
                updatingUI = false;
            }   
        }

    }
}
