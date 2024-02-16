using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectUtils
{
    public abstract class BaseMapping<T>
    {
        private bool running;

        //public EventHandler<OnMapping> OnMapping { get; set; }

        public void SubscribeToStartGesture(BaseGesture gesture)
        {

        }

        public void SubscribeToEndGesture(BaseGesture gesture)
        {

        }

        public void SubscribeToToggleGesture(BaseGesture gesture)
        {

        }

        protected abstract T Mapping(Body body);

        /*bool TestMapping(Body body, out T ouput)
        {
        }*/

        protected void OnBodyFrameArrived(object obj, BodyFrameArrivedEventArgs args)
        {

        }
    }
}
