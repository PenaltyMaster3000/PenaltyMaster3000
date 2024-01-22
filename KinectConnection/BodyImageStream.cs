using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KinectConnection
{
    public class BodyImageStream : KinectStream
    {
        private BodyFrameReader reader;
        private Body[] bodies = null;

        public Body[] Bodies
        {
            get { return bodies; }
            set { SetProperty(ref bodies, value); }
        }

        public BodyImageStream() : base()
        {
            // Initialize the bodies array
            this.bodies = new Body[this.KinectSensor.BodyFrameSource.BodyCount];
        }

        public override void Start()
        {
            // Open the reader for the body frames
            this.reader = this.KinectSensor.BodyFrameSource.OpenReader();

            // Subscribe to the event
            this.reader.FrameArrived += this.Reader_BodyFrameArrived;
        }

        public override void Stop()
        {
            if (this.reader != null)
            {
                // Unsubscribe from the event
                this.reader.FrameArrived -= this.Reader_BodyFrameArrived;

                // Dispose the reader to free resources
                this.reader.Dispose();
                this.reader = null;
            }
        }

        private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                }
            }

            // Process the body data
            foreach (Body body in this.bodies)
            {
                if (body.IsTracked)
                {
                    // Get the right hand joint
                    Joint rightHand = body.Joints[JointType.HandRight];

                    // Check the tracking state of the joint
                    if (rightHand.TrackingState == TrackingState.Tracked)
                    {
                        CameraSpacePoint position = rightHand.Position;
                    }
                }
            }
        }
    }
}
