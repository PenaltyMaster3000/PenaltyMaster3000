﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Kinect;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectConnection
{
    /// <summary>
    /// Classe représentant un flux d'image coloré pour la Kinect.
    /// Étend la classe KinectStream.
    /// </summary>
    public class ColorImageStream : KinectStream
    {
        /// <summary>
        /// The writeable bitmap.
        /// </summary>
        private WriteableBitmap bitmap = null;

        /// <summary>
        /// Obtient la source d'image de la classe.
        /// </summary>
        public override ImageSource Source
        {
            get { return this.bitmap; }
        }

        /// <summary>
        /// The color frame reader.
        /// </summary>
        private ColorFrameReader reader;

        /// <summary>
        /// Initialise une nouvelle instance de la classe ColorImageStream.
        /// </summary>
        public ColorImageStream() : base()
        {
            // create the colorFrameDescription from the ColorFrameSource using rgba format
            // the dimensions of the bitmap => match the dimensions of the color frame from the Kinect sensor.
            FrameDescription colorFrameDescription = this.KinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
            this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);

        }

        /// <summary>
        /// Démarre la lecture du flux coloré.
        /// </summary>
        public override void Start()
        {
            if (this.KinectSensor != null)
            {
                this.reader = this.KinectSensor.ColorFrameSource.OpenReader();

                if (this.reader != null)
                {
                    this.reader.FrameArrived += this.Reader_ColorFrameArrived;
                }
            }
        }

        /// <summary>
        /// Arrête la lecture du flux coloré.
        /// </summary>
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
        /// Méthode appelée lors de l'arrivée d'un nouveau frame coloré.
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            bool colorFrameProcessed = false;

            this.bitmap.Lock();

            // ColorFrame is IDisposable
            // We get the frame from the event
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    // I get the frame description
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    // verify data and write the new color frame data to the Writeable bitmap
                    if ((colorFrameDescription.Width == this.bitmap.PixelWidth) && (colorFrameDescription.Height == this.bitmap.PixelHeight))
                    {
                        // Explication du calcul : 
                        // width * height = total de pixels dans l'image
                        // PixelFormats.Bgr32.BitsPerPixel = rapport bits/pixel
                        // diviser par 8 pour avoir le nombre de bytes par pixel
                        byte[] colorDataArray = new byte[colorFrameDescription.Width * colorFrameDescription.Height * ((PixelFormats.Bgr32.BitsPerPixel + 7) / 8)];


                        if (colorFrame.RawColorImageFormat == ColorImageFormat.Bgra)
                        {
                            colorFrame.CopyRawFrameDataToArray(colorDataArray);
                        }
                        else
                        {
                            colorFrame.CopyConvertedFrameDataToArray(colorDataArray, ColorImageFormat.Bgra);
                        }

                        // Write the color data to the bitmap 
                        // Int32Rect : area within the bitmap to update => in this case all
                        this.bitmap.WritePixels(
                            new Int32Rect(0, 0, this.bitmap.PixelWidth, this.bitmap.PixelHeight),
                            colorDataArray,
                            this.bitmap.PixelWidth * sizeof(int),
                            0);

                        // flag that the frame was processed
                        colorFrameProcessed = true;
                    }
                }
            }

            // we got a frame, render
            if (colorFrameProcessed)
            {
                // ! Method not found
                this.bitmap.Unlock();
            }
        }
    }
}
