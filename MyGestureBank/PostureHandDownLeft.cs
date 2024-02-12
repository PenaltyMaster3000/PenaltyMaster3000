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
            GestureName = "HandDownLeft";
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

            bool handLeft = body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.HipLeft].Position.Y;

            var result = handLeft &&
                body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.HipRight].Position.Y &&
                body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y;

            return result;
        }
    }
}
