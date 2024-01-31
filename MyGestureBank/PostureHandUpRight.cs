using KinectUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

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
        /// The test gesture method.
        /// </summary>
        /// <param name="body"></param>
        public override void TestGesture(Body body)
        {
            if(TestPosture(body))
            {
                OnGestureRecognized();
            }  
        }

        protected override bool TestPosture(Body body)
        {
            return body.Joints[JointType.HandRight].Position.Y > body.Joints[JointType.ShoulderRight].Position.Y;
        }
    }
}
