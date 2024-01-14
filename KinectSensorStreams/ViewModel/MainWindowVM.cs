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

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructeur du ViewModel de la page principale
        /// </summary>
        public MainWindowVM()
        {
            KinectManager = new KinectManager();
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
            KinectManager.StartSensor();
        }

        #endregion
    }
}
