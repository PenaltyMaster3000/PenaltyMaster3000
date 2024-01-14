using Microsoft.Kinect;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectConnection
{
    /// <summary>
    /// The color image stream.
    /// </summary>
    public class ColorImageStream : KinectStream
    {
        /// <summary>
        /// The writeable bitmap.
        /// </summary>
        private WriteableBitmap bitmap = null;

        /// <summary>
        /// The color frame reader.
        /// </summary>
        private ColorFrameReader reader;

        public override void Start()
        {
            // create the colorFrameDescription from the ColorFrameSource using rgba format
            // the dimensions of the bitmap => match the dimensions of the color frame from the Kinect sensor.
            FrameDescription colorFrameDescription = this.KinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
            this.bitmap = this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);

            // open the reader for the color frames
            this.reader = this.KinectSensor.ColorFrameSource.OpenReader();
            // subscribe to the event
            this.reader.FrameArrived += this.Reader_ColorFrameArrived;
        }

        public override void Stop()
        {
            if (this.reader != null)
            {
                this.reader.FrameArrived -= this.Reader_ColorFrameArrived;

                // Dispose the reader to free resources.
                // If we don't dispose manualy, the gc will do it for us, but we don't know when.
                this.reader.Dispose();
                this.reader = null;
            }
        }

        /// <summary>
        /// METHOD FROM THE SAMPLE
        /// Handles the color frame data arriving from the sensor.
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            bool colorFrameProcessed = false;

            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    // verify data and write the new color frame data to the Writeable bitmap
                    if ((colorFrameDescription.Width == this.bitmap.PixelWidth) && (colorFrameDescription.Height == this.bitmap.PixelHeight))
                    {
                        if (colorFrame.RawColorImageFormat == ColorImageFormat.Bgra)
                        {
                            // ! Method not found
                            //colorFrame.CopyRawFrameDataToBuffer(this.bitmap.PixelBuffer);
                        }
                        else
                        {
                            // ! Method not found
                            //colorFrame.CopyConvertedFrameDataToBuffer(this.bitmap.PixelBuffer, ColorImageFormat.Bgra);
                        }

                        colorFrameProcessed = true;
                    }
                }
            }

            // we got a frame, render
            if (colorFrameProcessed)
            {
                // ! Method not found
                //this.bitmap.Invalidate();
            }
        }
    }
}
