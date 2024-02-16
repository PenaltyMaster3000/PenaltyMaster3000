using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinectConnection;
using KinectConnection.enums;
using KinectUtils;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Input;

namespace KinectSensorStreams.ViewModel
{
    public class MainWindowVM : ObservableObject
    {
        private BodyImageStream bodyImageStream;

        #region Properties

        /// <summary>
        /// Propriété liée à la commande appelée au démarrage de la page principale
        /// </summary>
        public ICommand StartCommand { get; set; }

        public ICommand BodyCommand { get; set; }

        /// <summary>
        /// The Kinect stream property.
        /// </summary>
        public BodyImageStream BodyImageStream
        {
            get { return bodyImageStream; }
            set { SetProperty(ref bodyImageStream, value); }
        }

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructeur du ViewModel de la page principale
        /// </summary>
        public MainWindowVM()
        {   
            StartCommand = new RelayCommand(Start);
            BodyCommand = new RelayCommand(Body);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Méthode initialisée au lancement de la page principale pour savoir si le Kinect est disponible ou non
        /// </summary>
        private void Start()
        {
            GestureManager.KinectManager.StartSensor();
        }

        private void Body()
        {
            if (BodyImageStream != null)
            {
                BodyImageStream.Stop();
            }
            BodyImageStream = new BodyImageStream();
            BodyImageStream.Start();
            //GestureManager.GestureRecognized += GestureManager.KnownGestures.FirstOrDefault().TestGesture();
        }

        #endregion
    }
}
