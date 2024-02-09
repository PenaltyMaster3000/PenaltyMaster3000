using KinectUtils;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGestureBank
{
    /// <summary>
    /// The left hand up posture.
    /// </summary>
    public class PostureHandUpLeft : Posture
    {
        public PostureHandUpLeft()
        {
            GestureName = "HandUpLeft";
        }

        /// <summary>
        /// The test gesture method.
        /// </summary>
        /// <param name="body">The body</param>
        public override void TestGesture(Body body)
        {
            if (TestPosture(body))
            {
                Console.WriteLine("Gesture recognized, hand up left");
                Thread.Sleep(1000);

                OnGestureRecognized();
            }
        }

        /// <summary>
        /// The test posture method.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override bool TestPosture(Body body)
        {
            // Check if the left hand is above the left shoulder
            return body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.SpineShoulder].Position.Y &&
                body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y;
        }
    }
}
