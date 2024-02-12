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
    public class PostureHandDownRight : Posture
    {
        public PostureHandDownRight()
        {
            GestureName = "HandDownRight";
        }

        /// <summary>
        /// The test posture method.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override bool TestPosture(Body body)
        {
            // Check if right hand is below the right hip
            var result = body.Joints[JointType.HandRight].Position.Y < body.Joints[JointType.HipRight].Position.Y &&
                body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.HipLeft].Position.Y &&
                body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y;

            return result;
        }
    }
}
