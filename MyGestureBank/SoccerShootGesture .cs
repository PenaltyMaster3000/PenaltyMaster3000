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
    /// The soccer shoot gesture.
    /// </summary>
    public class SoccerShootGesture : Gesture
    {
        private CameraSpacePoint lastLeftFootPosition;
        private CameraSpacePoint lastRightFootPosition;

        public SoccerShootGesture()
        {
            MinNbOfFrames = 10;
            MaxNbOfFrames = 30;
        }

        /// <summary>
        /// Tests if the gesture is recognized.
        /// </summary>
        /// <param name="body"></param>
        public override void TestGesture(Body body)
        {
            if (TestPosture(body))
            {
                Console.WriteLine("Gesture recognized, shooting motion");
                OnGestureRecognized();
            }
        }

        /// <summary>
        /// Tests the end conditions of the gesture.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        protected override bool TestEndConditions(Body body)
        {
            float threshold = 0.05f;

            bool areFeetClose = Math.Abs(body.Joints[JointType.FootLeft].Position.X - body.Joints[JointType.FootRight].Position.X) < threshold;
            return areFeetClose;
        }

        /// <summary>
        /// Tests the intial conditions of the gesture.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        protected override bool TestInitialConditions(Body body)
        {
            // Position of the feet + hips and head
            CameraSpacePoint currentLeftFootPosition = body.Joints[JointType.FootLeft].Position;
            CameraSpacePoint currentRightFootPosition = body.Joints[JointType.FootRight].Position;
            CameraSpacePoint currentHipPosition = body.Joints[JointType.SpineBase].Position;
            CameraSpacePoint currentHeadPosition = body.Joints[JointType.Head].Position;

            // X and Y that should be respected
            bool isWithinDistanceX = Math.Abs(currentLeftFootPosition.X - currentRightFootPosition.X) < Math.Abs(currentHipPosition.Y - currentHeadPosition.Y);
            bool isWithinRangeY = IsFootBetweenHeadAndSpinBase(body.Joints[JointType.FootRight].Position, body.Joints[JointType.Head].Position, body.Joints[JointType.SpineBase].Position);

            return isWithinDistanceX && isWithinRangeY;
        }

        /// <summary>
        /// Test the posture
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        protected override bool TestPosture(Body body)
        {
            bool isWithinRangeY = IsFootBetweenHeadAndSpinBase(body.Joints[JointType.FootRight].Position, body.Joints[JointType.Head].Position, body.Joints[JointType.SpineBase].Position);
            return isWithinRangeY;
        }

        /// <summary>
        /// Tests the running gesture
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        protected override bool TestRunningGesture(Body body)
        {
            CameraSpacePoint currentLeftFootPosition = body.Joints[JointType.FootLeft].Position;
            CameraSpacePoint currentRightFootPosition = body.Joints[JointType.FootRight].Position;

            bool areFeetCloserThanLastFrame =
                Math.Abs(currentLeftFootPosition.X) <= lastLeftFootPosition.X + 0.1f &&
                Math.Abs(currentRightFootPosition.X) <= lastRightFootPosition.X + 0.1f;

            lastLeftFootPosition = currentLeftFootPosition;
            lastRightFootPosition = currentRightFootPosition;

            return areFeetCloserThanLastFrame;
        }

        private bool IsFootBetweenHeadAndSpinBase(CameraSpacePoint footPosition, CameraSpacePoint headPosition, CameraSpacePoint hipPosition)
        {
            float maxY = headPosition.Y;
            float minY = hipPosition.Y;

            return footPosition.Y < maxY && footPosition.Y > minY;
        }
    }
}
