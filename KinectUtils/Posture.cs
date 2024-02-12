using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectUtils
{
    /// <summary>
    /// The posture class.
    /// </summary>
    public abstract class Posture : BaseGesture
    {
        private bool postureWasRecognized = false;

        /// <summary>
        /// Tests the posture.
        /// </summary>
        /// <param name="body">The body</param>
        /// <returns></returns>
        protected abstract bool TestPosture(Body body);

        /// <summary>
        /// The test gesture method.
        /// </summary>
        /// <param name="body">The body</param>
        public override void TestGesture(Body body)
        {
            postureWasRecognized = TestPosture(body);
            if (postureWasRecognized)
            {
                OnGestureRecognized();
            }
        }
    }
}
