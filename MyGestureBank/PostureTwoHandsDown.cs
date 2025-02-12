﻿using KinectUtils;
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
            GestureName = "HandsMid";
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
