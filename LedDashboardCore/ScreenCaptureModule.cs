using DesktopDuplication;
using SharpDX.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LedDashboardCore
{
    public static class ScreenCaptureModule
    {
        static bool isInitialized;
        static DesktopDuplicator desktopDuplicator;
        private static void Initialize()
        {
            //SharpDX.Configuration.EnableObjectTracking = true;
            // DesktopDuplication
            try
            {
                desktopDuplicator = new DesktopDuplicator(0);
                isInitialized = true;
                Debug.WriteLine("Desktop Duplication API initialized");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred initializing the screen capture module.\nException: \n" + ex.ToString());
            }
        }

        /// <summary>
        /// Gets the next screen frame. DISPOSE of the bitmap when you're done using it!
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetNextFrame()
        {
            if (!isInitialized)
                Initialize();
            try
            {
                DesktopFrame frame = desktopDuplicator.GetLatestFrame();
                if (frame != null)
                {
                    Bitmap frameBitmap = frame.DesktopImage;
                    return frameBitmap;

                }
            }
            catch (Exception)
            {
                //Thread.Sleep(200);
                desktopDuplicator.Dispose();
                desktopDuplicator = new DesktopDuplicator(0);
                Debug.WriteLine("Exception in DesktopDuplication API occurred"); // TODO: Enormous memory leak here sometimes
                //Debug.WriteLine(ObjectTracker.ReportActiveObjects());
               // throw;
            }
            return null;
        }

        /// <summary>
        /// Returns screen resolution. Returns empty rectangle if there is an error.
        /// </summary>
        public static Rectangle ScreenResolution
        {
            get
            {
                if (!isInitialized)
                    Initialize();
                try
                {
                    DesktopFrame frame = desktopDuplicator.GetLatestFrame();
                    if (frame != null)
                    {
                        Bitmap frameBitmap = frame.DesktopImage;

                        return new Rectangle(0, 0, frameBitmap.Width, frameBitmap.Height);

                    }
                }
                catch (Exception)
                {
                    desktopDuplicator = new DesktopDuplicator(0);
                    Debug.WriteLine("Exception in DesktopDuplication API occurred");
                }
                return new Rectangle(0,0,0,0);
            }
            
        }

    }
}
