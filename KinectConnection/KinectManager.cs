using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KinectConnection
{
    /// <summary>
    /// Classe KinectManager permettant la gestion des évènements autour du Kinect
    /// </summary>
    public class KinectManager : ObservableObject
    {
        private KinectSensor kinectSensor;
        private bool status;
        private string statusText;

        /// <summary>
        /// Constructeur de la classe KinectManager
        /// </summary>
        public KinectManager()
        {
            this.kinectSensor = KinectSensor.GetDefault();
            this.Status = false;
        }

        /// <summary>
        /// Propriété définissant le statut de l'état actuel du Kinect sous la forme d'un booléen
        /// </summary>
        public bool Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        /// <summary>
        /// Propriété définissant le statut de l'état actuel du Kinect sous la forme d'un string
        /// </summary>
        public string StatusText
        {
            get { return statusText; }
            set { SetProperty(ref statusText, value); }
        }

        /// <summary>
        /// Méthode permettant de lancer la méthode Open du KinectSensor et de s'abonner à l'évènement "IsAvailableChanged"
        /// </summary>
        public void StartSensor()
        {
            this.kinectSensor.Open();
            this.kinectSensor.IsAvailableChanged += this.KinectSensor_IsAvailableChanged;
        }

        /// <summary>
        /// Méthode permettant de lancer la méthode Close du KinectSensor et de se désabonner à l'évènement "IsAvailableChanged"
        /// </summary>
        public void StopSensor()
        {
            this.kinectSensor.IsAvailableChanged -= this.KinectSensor_IsAvailableChanged;
            this.kinectSensor.Close();
        }

        /// <summary>
        /// Évènement IsAvailableChanged permettant de mettre à jour les propriétés du KinectManager en fonction de l'état du kinect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void KinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs args)
        {
            this.StatusText = this.kinectSensor.IsAvailable ? "Kinect Available" : "Kinect Not Available";
            this.Status = this.kinectSensor.IsAvailable;
        }
    }
}
