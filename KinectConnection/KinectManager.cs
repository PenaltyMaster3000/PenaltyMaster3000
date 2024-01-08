using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KinectConnection
{
    public class KinectManager 
    {

        KinectManager()
        {
            this.kinectSensor = KinectSensor.GetDefault();
        }

        // properties
        public KinectSensor kinectSensor;

        public bool Status;
        
        public string StatusText; 

        // methods
        public void StartSensor()
        {
            this.kinectSensor.Open();
        }

        public void StopSensor()
        {
            this.kinectSensor.Close();
        }

        private void KinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs args)
        {
            this.StatusText = this.kinectSensor.IsAvailable ? "Kinect Available" : "Kinect Not Available";
        }

    }
}
