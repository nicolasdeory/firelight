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
using System.IO;

namespace LedDashboard
{

    // This is a debug UI window that displays the LED strip.

    class MainUI : Form
    {
        PictureBox box;
        Graphics canvas;
        LedManager ledManager;
        //FormsPlot plt;
        Button chromabtn;
        Button ledstripbtn;
        ComboBox lolModeSelector;

        List<KeyboardKey> keyboardLayout;
        LightingMode currentLightingMode = LightingMode.Keyboard;
        LightingMode lastDrawnMode = LightingMode.Keyboard;

        public MainUI()
        {

            keyboardLayout = LoadKeyboardLayout();

            this.Size = new Size(1600, 500);
            box = new PictureBox();
            box.Size = new Size(1600, 250);
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

            Label lolModeLabel = new Label();
            lolModeLabel.Text = "League of Legends Ability Cast mode";
            lolModeLabel.Location = new Point(300, 280);
            lolModeLabel.Size = new Size(200, 20);
            lolModeSelector = new ComboBox();
            lolModeSelector.Location = new Point(300, 300);
            lolModeSelector.Items.Add("Normal Cast");
            lolModeSelector.Items.Add("Quick Cast");
            lolModeSelector.Items.Add("Quick Cast with Indicator");
            lolModeSelector.SelectedIndex = 0;
            lolModeSelector.SelectedIndexChanged += LolModeSelectionChanged;

            /*plt = new FormsPlot();
            plt.Location = new Point(0, 100);
            plt.Size = new Size(800, 200);*/



            this.Controls.AddRange(new Control[] { box, chromabtn, ledstripbtn, lolModeLabel, lolModeSelector });

            Gradient.GeneratePalettes();

            ledManager = new LedManager();
            ledManager.DisplayUpdated += UpdateUI;

        }

        private void LolModeSelectionChanged(object sender, EventArgs e)
        {
            if(lolModeSelector.SelectedIndex == 0)
            {
                ledManager.SetModuleOption("lol", "castMode", "normal");
            } else if (lolModeSelector.SelectedIndex == 1) {
                ledManager.SetModuleOption("lol", "castMode", "quick");
            } else if (lolModeSelector.SelectedIndex == 2)
            {
                ledManager.SetModuleOption("lol", "castMode", "quickindicator");
            }
        }

        bool updatingUI = false;
        /// <summary>
        /// Update the virtual LED strip with the given LED color data.
        /// </summary>
        public void UpdateUI(Led[] leds, LightingMode mode)
        {
            if(!updatingUI)
            {
                updatingUI = true;
                if (currentLightingMode == LightingMode.Keyboard)
                {
                    if (mode == LightingMode.Keyboard)
                    {
                        if (lastDrawnMode != LightingMode.Keyboard)
                        {
                            canvas.Clear(Color.White);
                        }
                        int i = 0;
                        foreach (var led in leds)
                        {
                            byte[] col = led.color.ToRGB();
                            SolidBrush colBrush = new SolidBrush(Color.FromArgb(col[0], col[1], col[2]));
                            KeyboardKey key = keyboardLayout[i];
                            canvas.FillRectangle(colBrush, new Rectangle(key.X, key.Y, (key.Width ?? 20)-2 , (key.Height ?? 20)-2));
                            i++;
                        }
                        lastDrawnMode = LightingMode.Keyboard;
                    } else
                    {
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        if (lastDrawnMode != LightingMode.Line)
                        {
                            for (int i = 0; i < 88; i++)
                            {
                                KeyboardKey key = keyboardLayout[i];
                                canvas.FillRectangle(blackBrush, new Rectangle(key.X, key.Y, (key.Width ?? 20) - 2, (key.Height ?? 20) - 2));
                                i++;
                            }
                        }
                        for (int i = 0; i < leds.Length; i++)
                        {
                            byte[] col = leds[i].color.ToRGB();
                            // SolidBrush colBrush = new SolidBrush(Color.FromArgb((int)(leds[i].color.v*255), col[0], col[1], col[2]));
                            SolidBrush colBrush = new SolidBrush(Color.FromArgb(col[0], col[1], col[2]));
                            int x = (int)Utils.Scale(i, 0, leds.Length, 0, 22);
                            for(int j = 0; j < 6; j++)
                            {
                                canvas.FillRectangle(colBrush, new Rectangle(x * 20, j * 20, 18, 18));
                            }
                            
                        }
                        lastDrawnMode = LightingMode.Line;
                    }

                } else
                {
                    int i = 0;
                    foreach (var led in leds)
                    {
                        byte[] col = led.color.ToRGB();
                        SolidBrush colBrush = new SolidBrush(Color.FromArgb(col[0], col[1], col[2]));
                        canvas.FillRectangle(colBrush, new Rectangle(0 + i * 10, 0, 10, 10));
                        i++;
                    }
                    lastDrawnMode = LightingMode.Line;
                }

                updatingUI = false;
            }   
        }

        public void UseRazerChromaClicked(object s, EventArgs e)
        {
            ledManager.SetController(LightControllerType.RazerChroma);
            chromabtn.Enabled = false;
            ledstripbtn.Enabled = true;
            currentLightingMode = LightingMode.Keyboard;
            canvas.Clear(Color.White);
        }

        public void UseLEDStripClicked(object s, EventArgs e)
        {
            ledManager.SetController(LightControllerType.LED_Strip, 170, true); // This is for my LED strip atm (170 leds, in reverse order)
            chromabtn.Enabled = true;
            ledstripbtn.Enabled = false;
            currentLightingMode = LightingMode.Line;
            canvas.Clear(Color.White);
        }

        private List<KeyboardKey> LoadKeyboardLayout()
        {
            string json = "";
            try
            {
                json = File.ReadAllText(@"Resources/keyboardLayout.json");
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                throw new ArgumentException("File does not exist.");
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeyboardKey>>(json);
        }
    }
}
