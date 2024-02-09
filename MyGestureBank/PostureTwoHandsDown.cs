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
    /// The two in the middle.
    /// </summary>
    public class PostureTwoHandsDown : Posture
    {
        public PostureTwoHandsDown()
        {
            GestureName = "Both hands middle.";
        }

        /// <summary>
        /// The test gesture method.
        /// </summary>
        /// <param name="body">The body</param>
        public override void TestGesture(Body body)
        {
            if (TestPosture(body))
            {
                Console.WriteLine("Gesture recognized, both hands middle");
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
            // Check if two hands are up
            return body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y &&
                body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.HipRight].Position.Y && 
                body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y &&
                body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.HipLeft].Position.Y;
        }
    }
}
