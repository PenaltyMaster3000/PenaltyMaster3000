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
    /// The right hand down posture.
    /// </summary>
    public class PostureHandDownLeft : Posture
    {
        public PostureHandDownLeft()
        {
            GestureName = "Hand Down Left";
        }

        /// <summary>
        /// The test gesture method.
        /// </summary>
        /// <param name="body">The body</param>
        public override void TestGesture(Body body)
        {
            if (TestPosture(body))
            {
                Console.WriteLine("Gesture recognized, hand down left");
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
            // Check if the left hand is below the left hip
            return body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.HipLeft].Position.Y;
        }
    }
}
