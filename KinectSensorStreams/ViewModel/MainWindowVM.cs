using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinectConnection;
using KinectConnection.enums;
using System.Net.NetworkInformation;
using System.Windows.Input;

namespace KinectSensorStreams.ViewModel
{
    public class MainWindowVM : ObservableObject
    {
        private KinectStream kinectStream;

        #region Properties

        /// <summary>
        /// Propriété liée à la commande appelée au démarrage de la page principale
        /// </summary>
        public ICommand StartCommand { get; set; }

        public ICommand ColorCommand { get; set; }
        public ICommand BodyCommand { get; set; }
        public ICommand IRCommand { get; set; }
        public ICommand DepthCommand { get; set; }
        public ICommand BodyColorCommand { get; set; }

        /// <summary>
        /// Propriété liée à l'objet KinectManager
        /// Maybe to remove now that we have the KinectStream attribute
        /// </summary>
        public KinectManager KinectManager { get; set; }


        /// <summary>
        /// The Kinect streams factory.
        /// </summary>
        public KinectStreamsFactory KinectStreamsFactory { get; set; }

        /// <summary>
        /// The Kinect stream property.
        /// </summary>
        public KinectStream KinectStream
        {
            get { return kinectStream; }
            set { SetProperty(ref kinectStream, value); }
        }

        /// <summary>
        /// The Secondary Kinect stream property.
        /// </summary>
        public KinectStream KinectStream2
        {
            get { return kinectStream; }
            set { SetProperty(ref kinectStream, value); }
        }

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructeur du ViewModel de la page principale
        /// </summary>
        public MainWindowVM()
        {
            // eventuellement a enlever :
            KinectManager = new KinectManager();

            // factory
            KinectStreamsFactory = new KinectStreamsFactory(new KinectManager());
            
            StartCommand = new RelayCommand(Start);
            // [Question] : StartCommand ici peut être mieux que BeginInit() dans MainWindow.xaml.cs ?
            ColorCommand = new RelayCommand(Color);
            BodyCommand = new RelayCommand(Body);
            IRCommand = new RelayCommand(IR);
            DepthCommand = new RelayCommand(Depth);
            BodyColorCommand = new RelayCommand(BodyColor);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Méthode initialisée au lancement de la page principale pour savoir si le Kinect est disponible ou non
        /// </summary>
        private void Start()
        {
            KinectManager.StartSensor();
            
            // Start the kinect sensor
            //KinectStream.KinectManager.StartSensor();
            // Start the color stream reader
            //KinectStream.Start();
        }

        private void Color()
        {
            if(KinectStream != null)
            {
                KinectStream.Stop();
            }
            if (KinectStream2 != null)
            {
                KinectStream2.Stop();
            }
            KinectStream = KinectStreamsFactory[KinectStreams.Color];
            KinectStream.Start();
        }

        private void Body()
        {
            if (KinectStream != null)
            {
                KinectStream.Stop();
            }
            if (KinectStream2 != null)
            {
                KinectStream2.Stop();
            }
            KinectStream = KinectStreamsFactory[KinectStreams.Body];
            KinectStream.Start();
        }

        private void IR()
        {
            if (KinectStream != null)
            {
                KinectStream.Stop();
            }
            if (KinectStream2 != null)
            {
                KinectStream2.Stop();
            }
            KinectStream = KinectStreamsFactory[KinectStreams.IR];
            KinectStream.Start();
        }

        private void Depth()
        {
            if (KinectStream != null)
            {
                KinectStream.Stop();
            }
            if (KinectStream2 != null)
            {
                KinectStream2.Stop();
            }
            KinectStream = KinectStreamsFactory[KinectStreams.Depth];
            KinectStream.Start();
        }

        private void BodyColor()
        {
            if (KinectStream != null)
            {
                KinectStream.Stop();
            }
            if (KinectStream2 != null)
            {
                KinectStream2.Stop();
            }
            KinectStream = KinectStreamsFactory[KinectStreams.Color];
            KinectStream.Start();            
            KinectStream2 = KinectStreamsFactory[KinectStreams.Body];
            KinectStream2.Start();
        }

        #endregion
    }
}
