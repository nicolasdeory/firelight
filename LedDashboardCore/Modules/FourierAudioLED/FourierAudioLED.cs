using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace FirelightCore.Modules.FourierAudioLED
{
    public class FourierAudioLED : LEDModule
    {
        /*AudioEngine audio;
        
        const float BASS_MAX_FREQ = 225;
        /*const float BASS_TOP_LIMIT = 1750000;
        const float MID_TOP_LIMIT = 450000;*/
        /* const float BASS_TOP_LIMIT = 10; //5;
         const float MID_TOP_LIMIT = 2; //3;
         const float LED_COLOR_MIX_SPREAD = 5;
         const float FADE_TO_BLACK_FACTOR = 0.3f;

         const double FFT_MAXFREQ = AudioEngine.RATE / 2;

         double FFT_SPACING_HZ;

         int ledCount;

         int lastIndex;
         int[] freqArray;

         Led[] leds;

         public event LEDModule.FrameReadyHandler NewFrameReady;

         public static LEDModule Create()
         {
             FourierAudioLED module = new FourierAudioLED();
             module.Initialize(170);
             return module;
         }
         public void Initialize(int ledCount)
         {
             audio = new AudioEngine();
             audio.Start();
             audio.NewData += OnNewAudioData;
             this.ledCount = ledCount;
             FFT_SPACING_HZ = FFT_MAXFREQ / ledCount;

             this.lastIndex = (int)(2500 / FFT_SPACING_HZ);
             this.freqArray = new int[this.lastIndex];
             for (int i = 0; i < freqArray.Length; i++)
             {
                 this.freqArray[i] = (int)Math.Floor(Utils.Scale(FFTUtil.GetFreqForIndex(i, FFT_SPACING_HZ), 0, 2500, 0, this.ledCount / 2 + 1));
             }
             leds = new Led[ledCount];
             for (int i = 0; i < ledCount; i++)
             {
                 leds[i] = new Led();
             }
         }

         public Led[] DoFrame() // TODO: Latency issues...
         {
             leds.FadeToBlackAllLeds(0.3f); // 0.3f * delta que es 20 ms se supone
             if (!newAudioAvailable)
             {
                 return this.leds;
             }
             newAudioAvailable = false;
             var y_in = FFTUtil.ProcessAudio(audio);
             if (y_in.Length < this.lastIndex)
             {
                 return this.leds;
             }
             var y = new double[this.lastIndex];
             Array.Copy(y_in, 0, y, 0, this.lastIndex);
             y = y.Select(x => x < 0 ? 0 : x).ToArray();
             double[] y_scaled = new double[this.lastIndex];

             for (int i = 0; i < this.lastIndex; i++)
             {
                 var freq = FFTUtil.GetFreqForIndex(i, FFT_SPACING_HZ);
                 var val = y[i];
                 var limit = freq <= BASS_MAX_FREQ ? BASS_TOP_LIMIT : MID_TOP_LIMIT;
                 double scaled = Utils.Scale(val, 0, limit, 0, 1);
                 y_scaled[i] = scaled;
             }

             HSVColor[] yColors = new HSVColor[this.lastIndex];
             for (int i = 0; i < this.lastIndex; i++)
             {
                 yColors[i] = UtilsLED.ValueToHSV(this.ledCount / 2, this.freqArray[i], y_scaled[i]);
             }

             for (int i = 0; i < this.lastIndex; i++)
             {
                 if (this.freqArray[i] == this.ledCount / 2)
                     break;
                 leds.AddSymmetricColorAroundLeds(this.freqArray[i], yColors[i]);
             }

             return this.leds;
         }


         bool newAudioAvailable = false;

         void OnNewAudioData()
         {
             DoFrame();
             LEDData data = LEDData.Empty;
             data.Strip = this.leds.CloneLeds();
             NewFrameReady?.Invoke(new LEDFrame(this, data, LightZone.Strip, true));
             newAudioAvailable = true;
         }

         public void Dispose()
         {
             audio.Stop();
         }*/


        public event LEDModule.FrameReadyHandler NewFrameReady;

        const int ledCount = LEDData.NUMLEDS_STRIP;


        const int db = 32;
        const int supersample = 0;
        const int cqtBins = 170;
       //const int cqtBins = 512;
        const float fMin = 25.95f;
        //const float fMax = 1000f;
        const float fMax = 4504.0f;


        AudioEngine audio;
        
        double[] _aWeightingLUT;

        const int cqtBufferSize = 32768 * 2;
        // const int cqtBufferSize = 2048 * 2;
        //const int cqtBufferSize = 16384 * 2;

        byte[] audioBuffer = new byte[cqtBufferSize];

        public static LEDModule Create()
        {
            return new FourierAudioLED();
        }

        private FourierAudioLED()
        {
            Initialize();
        }

        public void Initialize()
        {
            audio = new AudioEngine();
            audio.Start();
            audio.NewData += OnNewAudioData;

            
            //                MIDI note  16 ==   20.60 hz
            // Piano key  1 = MIDI note  21 ==   27.50 hz
            // Piano key 88 = MIDI note 108 == 4186.01 hz
            //                MIDI note 127 == 12543.8 hz
            
            int cqtSize = cqt_init(AudioEngine.RATE, cqtBins, db, fMin, fMax, supersample);

            if (cqtSize == 0)
            {
                throw new Exception("Error initializing CQT");
            }

            int[] cqtFreqs = new int[cqtBins];
            for (int i = 0; i < cqtBins; i++)
            {
                cqtFreqs[i] = cqt_bin_to_freq(i);
            }

            _aWeightingLUT = cqtFreqs.Select(f => 0.5f + 0.5f * GetAWeighting(f)).ToArray();

            

        }

        public List<LEDData> DoFrame() // TODO: Latency issues...
        {
            List<LEDData> frameDatas = new List<LEDData>();
            int frameSize = 4032;
            var audioBytes = new byte[frameSize];
            audio.bwp.Read(audioBytes, 0, frameSize);

            for (int j = 0; j < 4; j++)
            {
                LEDData ledData = LEDData.Empty;

                // int frameSize = AudioEngine.BUFFERSIZE;
                //int frameSize = 2016;
                //int frameSize = 8160;
                // int frameSize = 4092;
                //int frameSize = 32736;

                // int frameSize = 960;

                int subFrameSize = 1008;

                for (int i = cqtBufferSize - subFrameSize; i >= 0; i -= subFrameSize)
                {
                    if (i <= subFrameSize)
                    {
                        int dif = subFrameSize - i;
                        Buffer.BlockCopy(audioBuffer, dif, audioBuffer, 0, subFrameSize - dif);
                    }
                    else
                    {
                        Buffer.BlockCopy(audioBuffer, i, audioBuffer, (i - subFrameSize), subFrameSize);
                    }
                }
                
                Buffer.BlockCopy(audioBytes, j*subFrameSize, audioBuffer, audioBuffer.Length - frameSize, subFrameSize);

                const int BYTES_PER_POINT = 2; // incoming data is 16-bit (2 bytes per audio point)

                // create 32 bit int array ready to fill with 16-bit data
                //int graphPointCount = audioBuffer.Length / BYTES_PER_POINT;
                int graphPointCount = audioBuffer.Length / BYTES_PER_POINT;

                Int16[] int16arr = new Int16[graphPointCount];

                for (int i = 0; i < graphPointCount; i++)
                {
                    // read the int16 from the two bytes
                    //short val = BitConverter.ToInt16(audioBuffer, i * 2);
                    short val = BitConverter.ToInt16(audioBuffer, i * 2);
                    int16arr[i] = val;


                    // store the value in Ys as a percent (+/- 100% = 200%)
                    // pcm[i] = val / Math.Pow(2, 16) * 200.0;
                }

                // we need to change it to float32

                float[] float32Arr = int16arr.Select(x => ((float)x) * (1f / 32768.0f)).ToArray();

                float[] resultCqt = new float[cqtBins];

                cqt_calc(float32Arr, null);
                cqt_render_line(resultCqt);

                double[] weightedCqt = new double[cqtBins];

                for (int x = 0; x < cqtBins; x++)
                {
                    double weighting = true ? _aWeightingLUT[x] : 1;
                    double val = 255 * weighting * resultCqt[x]; //this.lib.getValue(this.cqtOutput + x * 4, 'float') | 0;
                                                                 //const h = val * hCoeff | 0;
                    weightedCqt[x] = val;
                }

                double[] cqtBinData = weightedCqt;

                double[] cqtNormalized = NormalizeCqt(cqtBinData);

                for (int i = 0; i < LEDData.NUMLEDS_STRIP; i++)
                {
                    ledData.Strip[i].Color(new HSVColor(0, 0, (float)Math.Min(cqtNormalized[i], 1)));
                }

                frameDatas.Add(ledData);
            }
            return frameDatas;

        }

        private double[] NormalizeCqt(double[] cqtBins)
        {
            double max = cqtBins.Max();
            double threshold = 20;
            return cqtBins.Select(x => x < threshold ? 0 : x / max).ToArray();
        }

       /* private double[] ProcessAudio()
        {
            
            
        }*/


        bool newAudioAvailable = false;

        void OnNewAudioData()
        {
            List<LEDData> frames = DoFrame();
            //data.Strip = this.leds.CloneLeds();
            for (int i = 0; i < frames.Count; i++)
            {
                NewFrameReady?.Invoke(new LEDFrame(this, frames[i], LightZone.Strip, i==0));
            }
            newAudioAvailable = true;
        }

        public void Dispose()
        {
            audio.Stop();
        }

        double GetAWeighting(int f)
        {
            int f2 = f * f;
            return 1.5 * 1.2588966 * 148840000 * f2 * f2 /
                ((f2 + 424.36) * Math.Sqrt((f2 + 11599.29) * (f2 + 544496.41)) * (f2 + 148840000));
        }


        [DllImport("ConstantQTransform.dll")]
        public static extern int cqt_init(int rate, int width, float volume, float freq_min, float freq_max, int super);

        [DllImport("ConstantQTransform.dll", CallingConvention = CallingConvention.Cdecl,
    SetLastError = true)]
        public static extern void cqt_calc(float[] input_L, float[] input_R);

        [DllImport("ConstantQTransform.dll")]
        public static extern int cqt_render_line(float[] outArr);

        [DllImport("ConstantQTransform.dll")]
        public static extern int cqt_set_volume(float volume);

        [DllImport("ConstantQTransform.dll")]
        public static extern int cqt_bin_to_freq(int bin);
    }
}
