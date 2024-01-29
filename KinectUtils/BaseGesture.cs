using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectUtils
{
    public abstract class BaseGesture
    {
        public BaseGesture() { }

        public string GestureName { get; set; }

        // <GestureRecognizedEventArgs>
        public EventHandler GestureRecognized { get; set; }

        public abstract void TestGesture(Body body);

        protected void OnGestureRecognized() { }
    }
}
