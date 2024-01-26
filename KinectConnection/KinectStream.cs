using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Kinect;
using System.Windows.Media;

namespace KinectConnection
{
    /// <summary>
    /// Abstract class for Kinect streams.
    /// </summary>
    public abstract class KinectStream : ObservableObject
    {
        // Redondant d'avoir KinectSensor et KinectManager ici ? (car sensor dans manager)
        public KinectSensor KinectSensor { get; set; }
        
        public KinectManager KinectManager { get; set; }

        public abstract ImageSource Source { get; }

        public abstract void Start();

        public abstract void Stop();

        public KinectStream()
        {
            KinectSensor = KinectSensor.GetDefault();
            KinectManager = new KinectManager();
        }
    }
}
