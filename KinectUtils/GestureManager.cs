using KinectConnection;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KinectUtils
{
    public static class GestureManager
    {

        private static bool isAcquiringFrame = false; // false by default
        private static BodyFrameReader bodyFrameReader;

        // Properties
        public static KinectManager KinectManager {  get; set; } = new KinectManager();

        public static List<BaseGesture> KnownGestures { get; private set; } = new List<BaseGesture>();

        // <GestureRecognizedEventArgs>
        public static EventHandler GestureRecognized { get; set; }

        public static IGestureFactory Factory { get; set; }

        // Methods
        public static void AddGestures(IGestureFactory factory)
        {
            throw new NotImplementedException();
        }

        public static void AddGestures(BaseGesture[] baseGestures) // params ???
        {
            KnownGestures.AddRange(baseGestures);
        }

        public static void RemoveGesture(BaseGesture baseGesture) 
        {
            //baseGesture.GestureRecognized -= 
            KnownGestures.Remove(baseGesture);
        }

        public static void StartAcquiringFrames(KinectManager manager) 
        {
            if (!isAcquiringFrame)
            {
                manager.KinectSensor.Open();
                bodyFrameReader = manager.KinectSensor.BodyFrameSource.OpenReader();
                bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;
                isAcquiringFrame = true;
            }
        }

        public static void StopAcquiringFrame(KinectManager manager)
        {
            if (isAcquiringFrame)
            {
                manager.KinectSensor.Close();
                bodyFrameReader.FrameArrived -= BodyFrameReader_FrameArrived;
                isAcquiringFrame = false;
            }
        }

        private static void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    Body[] bodies = new Body[bodyFrame.BodyCount];
                    bodyFrame.GetAndRefreshBodyData(bodies);

                    foreach (Body body in bodies)
                    {
                        if (body.IsTracked)
                        {
                            foreach (var gesture in KnownGestures)
                            {
                                if (gesture != null)
                                {
                                    gesture.TestGesture(body);
                                }
                                else
                                {
                                    Console.WriteLine("gesture null");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
