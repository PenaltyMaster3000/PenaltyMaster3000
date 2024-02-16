using KinectUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Threading;

namespace MyGestureBank
{
    /// <summary>
    /// The hand up right posture.
    /// </summary>
    public class PostureHandUpRight : Posture 
    {
        /// <summary>
        /// PostureHandUpRight constructor.
        /// </summary>
        public PostureHandUpRight()
        {
            GestureName = "HandUpRight";
        }

        /// <summary>
        /// Tests the posture.
        /// </summary>
        /// <param name="body"></param>
        /// <returns>A boolean indicating wheter the posture was detected or not.</returns>
        protected override bool TestPosture(Body body)
        {
            return body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.SpineShoulder].Position.Y &&
                body.Joints[JointType.HandLeft].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y &&
                body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.HipLeft].Position.Y;
        }
    }
}
