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
    public class KinectManager 
    {

        public KinectManager()
        {
            this.kinectSensor = KinectSensor.GetDefault();
        }

        // properties
        public KinectSensor kinectSensor;

        public bool Status;

        public event PropertyChangedEventHandler PropertyChanged;

        private string statusText;
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value) 
                {
                    this.statusText = value;

                    if(this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        } 

        // methods
        public void StartSensor()
        {
            this.kinectSensor.Open();
            this.kinectSensor.IsAvailableChanged += this.KinectSensor_IsAvailableChanged;
        }

        public void StopSensor()
        {
            this.kinectSensor.IsAvailableChanged -= this.KinectSensor_IsAvailableChanged;
            this.kinectSensor.Close();
        }

        private void KinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs args)
        {
            this.StatusText = this.kinectSensor.IsAvailable ? "Kinect Available" : "Kinect Not Available";
        }

    }
}
