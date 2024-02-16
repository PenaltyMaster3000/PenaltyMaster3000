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
    /// The two hands up posture.
    /// </summary>
    public class PostureTwoHandsUp : Posture
    {
        public PostureTwoHandsUp()
        {
            GestureName = "HandsUp";
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
            return body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ShoulderRight].Position.Y &&
                body.Joints[JointType.HandLeft].Position.Y > body.Joints[JointType.ShoulderLeft].Position.Y;
        }
    }
}
