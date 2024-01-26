using Microsoft.Kinect;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectConnection
{
    /// <summary>
    /// Classe représentant un flux d'image infrarouge pour la Kinect.
    /// Étend la classe KinectStream.
    /// </summary>
    public class InfraredImageStream : KinectStream
    {
        /// <summary>
        /// Maximum value (as a float) that can be returned by the InfraredFrame
        /// </summary>
        private const float InfraredSourceValueMaximum = (float)ushort.MaxValue;

        /// <summary>
        /// The value by which the infrared source data will be scaled
        /// </summary>
        private const float InfraredSourceScale = 0.75f;

        /// <summary>
        /// Smallest value to display when the infrared data is normalized
        /// </summary>
        private const float InfraredOutputValueMinimum = 0.01f;

        /// <summary>
        /// Largest value to display when the infrared data is normalized
        /// </summary>
        private const float InfraredOutputValueMaximum = 1.0f;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Reader for infrared frames
        /// </summary>
        private InfraredFrameReader infraredFrameReader = null;

        /// <summary>
        /// Description (width, height, etc) of the infrared frame data
        /// </summary>
        private FrameDescription infraredFrameDescription = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap infraredBitmap = null;

        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;

        /// <summary>
        /// Obtient la source d'image de la classe.
        /// </summary>
        public override ImageSource Source
        {
            get
            {
                return this.infraredBitmap;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InfraredImageStream.
        /// </summary>
        public InfraredImageStream()
        {
            // get FrameDescription from InfraredFrameSource
            this.infraredFrameDescription = this.KinectSensor.InfraredFrameSource.FrameDescription;

            // create the bitmap to display
            this.infraredBitmap = new WriteableBitmap(this.infraredFrameDescription.Width, this.infraredFrameDescription.Height, 96.0, 96.0, PixelFormats.Gray32Float, null);
        }

        /// <summary>
        /// Démarre la lecture du flux infrarouge.
        /// </summary>
        public override void Start()
        {
            if (this.KinectSensor != null)
            {
                // open the reader for the depth frames
                this.infraredFrameReader = this.KinectSensor.InfraredFrameSource.OpenReader();

                if (this.infraredFrameReader != null)
                {
                    // wire handler for frame arrival
                    this.infraredFrameReader.FrameArrived += this.Reader_InfraredFrameArrived;
                }
            }
        }

        /// <summary>
        /// Arrête la lecture du flux infrarouge.
        /// </summary>
        public override void Stop()
        {
            if (this.infraredFrameReader != null)
            {
                this.infraredFrameReader.FrameArrived -= this.Reader_InfraredFrameArrived;

                // Dispose the reader to free resources.
                // If we don't dispose manualy, the gc will do it for us, but we don't know when.
                this.infraredFrameReader.Dispose();
                this.infraredFrameReader = null;
            }
        }

        /// <summary>
        /// Méthode appelée lors de l'arrivée d'un nouveau frame infrarouge.
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_InfraredFrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            // InfraredFrame is IDisposable
            using (InfraredFrame infraredFrame = e.FrameReference.AcquireFrame())
            {
                if (infraredFrame != null)
                {
                    // the fastest way to process the infrared frame data is to directly access 
                    // the underlying buffer
                    using (Microsoft.Kinect.KinectBuffer infraredBuffer = infraredFrame.LockImageBuffer())
                    {
                        // verify data and write the new infrared frame data to the display bitmap
                        if (((this.infraredFrameDescription.Width * this.infraredFrameDescription.Height) == (infraredBuffer.Size / this.infraredFrameDescription.BytesPerPixel)) &&
                            (this.infraredFrameDescription.Width == this.infraredBitmap.PixelWidth) && (this.infraredFrameDescription.Height == this.infraredBitmap.PixelHeight))
                        {
                            this.ProcessInfraredFrameData(infraredBuffer.UnderlyingBuffer, infraredBuffer.Size);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Accède directement au tampon d'image sous-jacent du InfraredFrame pour 
        /// créer une bitmap affichable.
        /// Cette fonction nécessite l'option /unsafe du compilateur car nous utilisons un accès direct
        /// à la mémoire native pointée par le pointeur infraredFrameData.
        /// </summary>
        /// <param name="infraredFrameData">Pointeur vers les données d'image InfraredFrame</param>
        /// <param name="infraredFrameDataSize">Taille des données d'image InfraredFrame</param>
        private unsafe void ProcessInfraredFrameData(IntPtr infraredFrameData, uint infraredFrameDataSize)
        {
            // infrared frame data is a 16 bit value
            ushort* frameData = (ushort*)infraredFrameData;

            // lock the target bitmap
            this.infraredBitmap.Lock();

            // get the pointer to the bitmap's back buffer
            float* backBuffer = (float*)this.infraredBitmap.BackBuffer;

            // process the infrared data
            for (int i = 0; i < (int)(infraredFrameDataSize / this.infraredFrameDescription.BytesPerPixel); ++i)
            {
                // since we are displaying the image as a normalized grey scale image, we need to convert from
                // the ushort data (as provided by the InfraredFrame) to a value from [InfraredOutputValueMinimum, InfraredOutputValueMaximum]
                backBuffer[i] = Math.Min(InfraredOutputValueMaximum, (((float)frameData[i] / InfraredSourceValueMaximum * InfraredSourceScale) * (1.0f - InfraredOutputValueMinimum)) + InfraredOutputValueMinimum);
            }

            // mark the entire bitmap as needing to be drawn
            this.infraredBitmap.AddDirtyRect(new Int32Rect(0, 0, this.infraredBitmap.PixelWidth, this.infraredBitmap.PixelHeight));

            // unlock the bitmap
            this.infraredBitmap.Unlock();
        }
    }
}
