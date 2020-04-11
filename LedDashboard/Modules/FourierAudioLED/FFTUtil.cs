using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.FourierAudioLED
{
    class FFTUtil
    {

        public static double[] ProcessAudio(AudioEngine audio)
        {

            int frameSize = AudioEngine.BUFFERSIZE;
            var audioBytes = new byte[frameSize];
            audio.bwp.Read(audioBytes, 0, frameSize);
            if (audioBytes.Length == 0) return new double[0];
            if (audioBytes[frameSize - 2] == 0) return new double[0]; // Return if there's nothing new to plot

            const int BYTES_PER_POINT = 2; // incoming data is 16-bit (2 bytes per audio point)

            // create 32 bit int array ready to fill with 16-bit data
            int graphPointCount = audioBytes.Length / BYTES_PER_POINT;

            // create double arrays to hold data to graph
            double[] pcm = new double[graphPointCount];
            double[] fft = new double[graphPointCount];
            double[] fftReal = new double[graphPointCount / 2];

            // populate Xs and Ys with double data
            for (int i = 0; i < graphPointCount; i++)
            {
                // read the int16 from the two bytes
                Int16 val = BitConverter.ToInt16(audioBytes, i * 2);

                // store the value in Ys as a percent (+/- 100% = 200%)
                pcm[i] = (double)(val) / Math.Pow(2, 16) * 200.0;
            }
            double maxPCM = pcm.Max(x => Math.Abs(x));
            fft = FFT(pcm);

            // determine horizontal axis units for graphs
            double fftMaxFreq = AudioEngine.RATE / 2;
            double fftPointSpacingHz = fftMaxFreq / graphPointCount;

            // keep the real half only (the other one is imaginary)
            Array.Copy(fft, fftReal, fftReal.Length);

            return fftReal;

        }

        public static double GetFreqForIndex(int index, double fftSpacingHz)
        {
            return index * fftSpacingHz;
        }

        public static double[] FFT(double[] data)
        {
            double[] fft = new double[data.Length];
            Complex[] fftComplex = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++)
                fftComplex[i] = new System.Numerics.Complex(data[i], 0.0);
            FourierTransform.FFT(fftComplex, FourierTransform.Direction.Forward);
            for (int i = 0; i < data.Length; i++)
                fft[i] = fftComplex[i].Magnitude;
            return fft;
        }
    }
}
