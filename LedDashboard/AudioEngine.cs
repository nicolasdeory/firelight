using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numpy;
using Python.Runtime;
using System.Diagnostics;

namespace LedDashboard
{
    class AudioEngine
    {
        private WaveInEvent wi = null;

        public delegate void NewDataHandler();
        public event NewDataHandler NewData;


        public const int BUFFERSIZE = 1024;
        public const int RATE = 44100;

        private int sampleRate;

        public BufferedWaveProvider bwp;

        public void Start()
        {
            if (wi != null)
                return;

            int IN_DEVICE_INDEX = -1;

            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                Debug.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
                if (deviceInfo.ProductName.Contains("VoiceMeeter"))
                {
                    IN_DEVICE_INDEX = waveInDevice;
                    break;
                }
            }

            int OUT_DEVICE_INDEX = -1;

            int waveOutDevices = WaveOut.DeviceCount;
            for (int waveOutDevice = 0; waveOutDevice < waveOutDevices; waveOutDevice++)
            {
                WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(waveOutDevice);
                Debug.WriteLine("Device {0}: {1}, {2} channels", waveOutDevice, deviceInfo.ProductName, deviceInfo.Channels);
                if (deviceInfo.ProductName.Contains("VoiceMeeter" +
                    ""))
                {
                    OUT_DEVICE_INDEX = waveOutDevice;
                    break;
                }
            }

            // create wave input from mic
            wi = new WaveInEvent();
            wi.DeviceNumber = IN_DEVICE_INDEX;
            wi.WaveFormat = new WaveFormat(RATE,1);
            sampleRate = RATE;
            Debug.WriteLine("Running at a sample rate of " + sampleRate);
            wi.BufferMilliseconds = (int)((double)BUFFERSIZE / (double)RATE * 1000.0);
            wi.RecordingStopped += waveIn_RecordingStopped;
            wi.DataAvailable += waveIn_DataAvailable;

            bwp = new BufferedWaveProvider(wi.WaveFormat);
            bwp.BufferLength = BUFFERSIZE * 2;
            bwp.DiscardOnBufferOverflow = true;

            wi.StartRecording();
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // add received data to waveProvider buffer
            bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
            NewData?.Invoke();

        }

        public void Stop()
        {
            if (wi != null)
                wi.StopRecording();
        }

        void waveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            // stop playback

            // dispose of wave input
            if (wi != null)
            {
                wi.Dispose();
                wi = null;
            }
        }

    }
}
