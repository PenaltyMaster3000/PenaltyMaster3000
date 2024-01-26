﻿using CommunityToolkit.Mvvm.ComponentModel;
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
            KinectStream = KinectStreamsFactory[KinectStreams.Depth];

            StartCommand = new RelayCommand(Start);
            // [Question] : StartCommand ici peut être mieux que BeginInit() dans MainWindow.xaml.cs ?
            ColorCommand = new RelayCommand(Color);
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
            KinectStream.Start();
        }

        private void Color()
        {
            //KinectStream.Start();
        }

        #endregion
    }
}
