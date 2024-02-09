using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectUtils
{
    /// <summary>
    /// The base gesture.
    /// </summary>
    public abstract class BaseGesture
    {
        /// <summary>
        /// The base gesture initializer.
        /// </summary>
        public BaseGesture() { }

        /// <summary>
        /// The gesture name.
        /// </summary>
        public string GestureName { get; set; }

        /// <summary>
        /// The gesture recognized event.
        /// </summary>
        public EventHandler<GestureRecognizedEventArgs> GestureRecognized { get; set; }

        /// <summary>
        /// The test gesture.
        /// </summary>
        /// <param name="body"></param>
        public abstract void TestGesture(Body body);


        /// <summary>
        /// The on gesture recognized method.
        /// </summary>
        protected void OnGestureRecognized() 
        {
            GestureRecognized?.Invoke(this, new GestureRecognizedEventArgs(GestureName));
        }
    }
}
