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
    /// Manages Kinect events.
    /// </summary>
    public class KinectManager : ObservableObject
    {
        private KinectSensor kinectSensor;
        private bool status;
        private string statusText;

        /// <summary>
        /// Initializes a new instance of the KinectManager class.
        /// </summary>
        public KinectManager()
        {
            this.kinectSensor = KinectSensor.GetDefault();
            this.Status = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Kinect is available.
        /// </summary>
        public bool Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        /// <summary>
        /// Gets or sets the status text of the Kinect.
        /// </summary>
        public string StatusText
        {
            get { return statusText; }
            set { SetProperty(ref statusText, value); }
        }

        /// <summary>
        /// Starts the Kinect sensor and subscribes to the IsAvailableChanged event.
        /// </summary>
        public void StartSensor()
        {
            this.kinectSensor.Open();
            this.kinectSensor.IsAvailableChanged += this.KinectSensor_IsAvailableChanged;
        }

        /// <summary>
        /// Stops the Kinect sensor and unsubscribes from the IsAvailableChanged event.
        /// </summary>
        public void StopSensor()
        {
            this.kinectSensor.IsAvailableChanged -= this.KinectSensor_IsAvailableChanged;
            this.kinectSensor.Close();
        }

        /// <summary>
        /// Updates the KinectManager properties based on the Kinect's availability.
        /// </summary>
        private void KinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs args)
        {
            this.StatusText = this.kinectSensor.IsAvailable ? "Kinect Available" : "Kinect Not Available";
            this.Status = this.kinectSensor.IsAvailable;
        }
    }
}
