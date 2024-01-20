using CommunityToolkit.Mvvm.Input;
using KinectConnection;
using System.Windows.Input;

namespace KinectSensorStreams.ViewModel
{
    public class MainWindowVM
    {
        #region Properties

        /// <summary>
        /// Propriété liée à la commande appelée au démarrage de la page principale
        /// </summary>
        public ICommand StartCommand { get; set; }

        /// <summary>
        /// Propriété liée à l'objet KinectManager
        /// </summary>
        public KinectManager KinectManager { get; set; }


        /// <summary>
        /// The Kinect streams factory.
        /// </summary>
        public KinectStreamsFactory KinectStreamsFactory { get; set; }

        /// <summary>
        /// The Kinect stream property.
        /// </summary>
        public KinectStream KinectStream { get; set; }

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
            // kinect stream => color stream for now
            KinectStream = KinectStreamsFactory[KinectStreams.Color];


            StartCommand = new RelayCommand(Start);
            // [Question] : StartCommand ici peut être mieux que BeginInit() dans MainWindow.xaml.cs ?
        }

        #endregion

        #region Methods

        /// <summary>
        /// Méthode initialisée au lancement de la page principale pour savoir si le Kinect est disponible ou non
        /// </summary>
        private void Start()
        {
            //KinectManager.StartSensor();
            
            // Start the kinect sensor
            KinectStream.KinectManager.StartSensor();
            // Start the color stream reader
            KinectStream.Start();
        }

        #endregion
    }
}
