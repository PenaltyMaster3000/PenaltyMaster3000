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
    /// Classe représentant un flux d'image de profondeur pour la Kinect.
    /// Étend la classe KinectStream.
    /// </summary>
    public class DepthImageStream : KinectStream
    {
        /// <summary>
        /// Constante pour mapper la plage de profondeur à la plage de byte.
        /// </summary>
        private const int MapDepthToByte = 8000 / 256;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Reader for depth frames
        /// </summary>
        private DepthFrameReader depthFrameReader = null;

        /// <summary>
        /// Description of the data contained in the depth frame
        /// </summary>
        private FrameDescription depthFrameDescription = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap depthBitmap = null;

        /// <summary>
        /// Intermediate storage for frame data converted to color
        /// </summary>
        private byte[] depthPixels = null;

        /// <summary>
        /// Obtient la source d'image de la classe.
        /// </summary>
        public override ImageSource Source
        {
            get
            {
                return this.depthBitmap;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe DepthImageStream.
        /// </summary>
        public DepthImageStream() : base()
        {
            // get FrameDescription from DepthFrameSource
            this.depthFrameDescription = this.KinectSensor.DepthFrameSource.FrameDescription;

            // allocate space to put the pixels being received and converted
            this.depthPixels = new byte[this.depthFrameDescription.Width * this.depthFrameDescription.Height];

            // create the bitmap to display
            this.depthBitmap = new WriteableBitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, 96.0, 96.0, PixelFormats.Gray8, null);
        }

        /// <summary>
        /// Démarre la lecture du flux de profondeur.
        /// </summary>
        public override void Start()
        {
            if (this.KinectSensor != null)
            {
                // open the reader for the depth frames
                this.depthFrameReader = this.KinectSensor.DepthFrameSource.OpenReader();

                if (this.depthFrameReader != null)
                {
                    // wire handler for frame arrival
                    this.depthFrameReader.FrameArrived += this.Reader_FrameArrived;
                }
            }
        }

        /// <summary>
        /// Arrête la lecture du flux de profondeur.
        /// </summary>
        public override void Stop()
        {
            if (this.depthFrameReader != null)
            {
                this.depthFrameReader.FrameArrived -= this.Reader_FrameArrived;

                // Dispose the reader to free resources.
                // If we don't dispose manualy, the gc will do it for us, but we don't know when.
                this.depthFrameReader.Dispose();
                this.depthFrameReader = null;
            }
        }

        /// <summary>
        /// Méthode appelée lors de l'arrivée d'un nouveau frame de profondeur.
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            bool depthFrameProcessed = false;

            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame != null)
                {
                    // the fastest way to process the body index data is to directly access 
                    // the underlying buffer
                    using (Microsoft.Kinect.KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                    {
                        // verify data and write the color data to the display bitmap
                        if (((this.depthFrameDescription.Width * this.depthFrameDescription.Height) == (depthBuffer.Size / this.depthFrameDescription.BytesPerPixel)) &&
                            (this.depthFrameDescription.Width == this.depthBitmap.PixelWidth) && (this.depthFrameDescription.Height == this.depthBitmap.PixelHeight))
                        {
                            // Note: In order to see the full range of depth (including the less reliable far field depth)
                            // we are setting maxDepth to the extreme potential depth threshold
                            ushort maxDepth = ushort.MaxValue;

                            this.ProcessDepthFrameData(depthBuffer.UnderlyingBuffer, depthBuffer.Size, depthFrame.DepthMinReliableDistance, maxDepth);
                            depthFrameProcessed = true;
                        }
                    }
                }
            }

            if (depthFrameProcessed)
            {
                this.RenderDepthPixels();
            }
        }

        /// <summary>
        /// Accède directement au tampon d'image sous-jacent du DepthFrame
        /// pour créer une bitmap affichable.
        /// Cette méthode nécessite l'option du compilateur "/unsafe" pour avoir directement accès
        /// à la mémoire native pointée par le pointer depthFrameData.
        /// </summary>
        /// <param name="depthFrameData">Pointeur vers la DepthFrame image data</param>
        /// <param name="depthFrameDataSize">Taille de la DepthFrame image data</param>
        /// <param name="minDepth">La plus fiable valeur minimale pour la frame</param>
        /// <param name="maxDepth">La plus fiable valeur maximale pour la frame</param>
        private unsafe void ProcessDepthFrameData(IntPtr depthFrameData, uint depthFrameDataSize, ushort minDepth, ushort maxDepth)
        {
            // depth frame data is a 16 bit value
            ushort* frameData = (ushort*)depthFrameData;

            // convert depth to a visual representation
            for (int i = 0; i < (int)(depthFrameDataSize / this.depthFrameDescription.BytesPerPixel); ++i)
            {
                // Get the depth for this pixel
                ushort depth = frameData[i];

                // To convert to a byte, we're mapping the depth value to the byte range.
                // Values outside the reliable depth range are mapped to 0 (black).
                this.depthPixels[i] = (byte)(depth >= minDepth && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
            }
        }

        /// <summary>
        /// Rend les pixels de profondeur dans la bitmap.
        /// </summary>
        private void RenderDepthPixels()
        {
            this.depthBitmap.WritePixels(
                new Int32Rect(0, 0, this.depthBitmap.PixelWidth, this.depthBitmap.PixelHeight),
                this.depthPixels,
                this.depthBitmap.PixelWidth,
                0);
        }
    }
}
