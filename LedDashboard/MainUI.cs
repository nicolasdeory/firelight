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
        Button chromabtn;
        Button ledstripbtn;

        public MainUI()
        {
            this.Size = new Size(1600, 500);
            box = new PictureBox();
            box.Size = new Size(1600, 50);
            box.Location = new Point(0, 10);
            box.BackColor = Color.White;
            canvas = box.CreateGraphics();

            chromabtn = new Button();
            chromabtn.Text = "Use Razer Chroma";
            chromabtn.Size = new Size(200, 50);
            chromabtn.Location = new Point(0, 300);
            chromabtn.Click += UseRazerChromaClicked;
            chromabtn.Enabled = false;

            ledstripbtn = new Button();
            ledstripbtn.Text = "Use LED Strip";
            ledstripbtn.Size = new Size(200, 50);
            ledstripbtn.Location = new Point(0, 350);
            ledstripbtn.Click += UseLEDStripClicked;

            plt = new FormsPlot();
            plt.Location = new Point(0, 100);
            plt.Size = new Size(800, 200);

            this.Controls.AddRange(new Control[] { box, plt, chromabtn, ledstripbtn });

            Gradient.GeneratePalettes();

            ledManager = new LedManager();
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

        public void UseRazerChromaClicked(object s, EventArgs e)
        {
            ledManager.SetController(LightControllerType.RazerChroma);
            chromabtn.Enabled = false;
            ledstripbtn.Enabled = true;
        }

        public void UseLEDStripClicked(object s, EventArgs e)
        {
            ledManager.SetController(LightControllerType.LED_Strip, 170, true); // This is for my LED strip atm (170 leds, in reverse order)
            chromabtn.Enabled = true;
            ledstripbtn.Enabled = false;
        }

    }
}
