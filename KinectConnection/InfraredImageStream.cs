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
        /// Valeur maximale (en tant que float) que peut renvoyer InfraredFrame.
        /// </summary>
        private const float InfraredSourceValueMaximum = (float)ushort.MaxValue;

        /// <summary>
        /// La valeur par laquelle les données de la source infrarouge seront ajustées.
        /// </summary>
        private const float InfraredSourceScale = 0.75f;

        /// <summary>
        /// Plus petite valeur à afficher lorsque les données infrarouges sont normalisées.
        /// </summary>
        private const float InfraredOutputValueMinimum = 0.01f;

        /// <summary>
        /// Plus grande valeur à afficher lorsque les données infrarouges sont normalisées.
        /// </summary>
        private const float InfraredOutputValueMaximum = 1.0f;

        /// <summary>
        /// Capteur Kinect actif.
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Lecteur pour les images infrarouges.
        /// </summary>
        private InfraredFrameReader infraredFrameReader = null;

        /// <summary>
        /// Description (largeur, hauteur, etc.) des données du cadre infrarouge.
        /// </summary>
        private FrameDescription infraredFrameDescription = null;

        /// <summary>
        /// Bitmap à afficher.
        /// </summary>
        private WriteableBitmap infraredBitmap = null;

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
            // Obtient la description du cadre infrarouge à partir de InfraredFrameSource
            this.infraredFrameDescription = this.KinectSensor.InfraredFrameSource.FrameDescription;

            // Crée la bitmap à afficher
            this.infraredBitmap = new WriteableBitmap(this.infraredFrameDescription.Width, this.infraredFrameDescription.Height, 96.0, 96.0, PixelFormats.Gray32Float, null);
        }

        /// <summary>
        /// Démarre la lecture du flux infrarouge.
        /// </summary>
        public override void Start()
        {
            if (this.KinectSensor != null)
            {
                // Ouvre le lecteur pour les trames infrarouges
                this.infraredFrameReader = this.KinectSensor.InfraredFrameSource.OpenReader();

                if (this.infraredFrameReader != null)
                {
                    // Lie le gestionnaire pour l'arrivée de la trame
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

                // Libère le lecteur pour libérer des ressources.
                // Si nous ne le faisons pas manuellement, le GC le fera pour nous, mais nous ne savons pas quand.
                this.infraredFrameReader.Dispose();
                this.infraredFrameReader = null;
            }
        }

        /// <summary>
        /// Méthode appelée lors de l'arrivée d'un nouveau cadre infrarouge.
        /// </summary>
        /// <param name="sender">objet envoyant l'événement</param>
        /// <param name="e">arguments de l'événement</param>
        private void Reader_InfraredFrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            // InfraredFrame est IDisposable
            using (InfraredFrame infraredFrame = e.FrameReference.AcquireFrame())
            {
                if (infraredFrame != null)
                {
                    // La manière la plus rapide de traiter les données du cadre infrarouge est d'accéder directement
                    // au tampon sous-jacent
                    using (Microsoft.Kinect.KinectBuffer infraredBuffer = infraredFrame.LockImageBuffer())
                    {
                        // Vérifie les données et écrit les nouvelles données du cadre infrarouge sur la bitmap d'affichage
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
        /// <param name="infraredFrameData">Pointeur vers les données du cadre infrarouge</param>
        /// <param name="infraredFrameDataSize">Taille des données du cadre infrarouge</param>
        private unsafe void ProcessInfraredFrameData(IntPtr infraredFrameData, uint infraredFrameDataSize)
        {
            // Les données du cadre infrarouge sont une valeur de 16 bits
            ushort* frameData = (ushort*)infraredFrameData;

            // Verrouille la bitmap cible
            this.infraredBitmap.Lock();

            // Obtient le pointeur vers le tampon arrière de la bitmap
            float* backBuffer = (float*)this.infraredBitmap.BackBuffer;

            // Traite les données infrarouges
            for (int i = 0; i < (int)(infraredFrameDataSize / this.infraredFrameDescription.BytesPerPixel); ++i)
            {
                // Comme nous affichons l'image en niveaux de gris normalisés, nous devons convertir de
                // la donnée ushort (fournie par InfraredFrame) à une valeur de [InfraredOutputValueMinimum, InfraredOutputValueMaximum]
                backBuffer[i] = Math.Min(InfraredOutputValueMaximum, (((float)frameData[i] / InfraredSourceValueMaximum * InfraredSourceScale) * (1.0f - InfraredOutputValueMinimum)) + InfraredOutputValueMinimum);
            }

            // Marque toute la bitmap comme nécessitant un redessin
            this.infraredBitmap.AddDirtyRect(new Int32Rect(0, 0, this.infraredBitmap.PixelWidth, this.infraredBitmap.PixelHeight));

            // Déverrouille la bitmap
            this.infraredBitmap.Unlock();
        }
    }
}
