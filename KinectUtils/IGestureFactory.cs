using System;
using System.Collections.Generic;

namespace KinectUtils
{
    public interface IGestureFactory
    {
        IEnumerable<BaseGesture> CreateGestures();
        BaseGesture this[string name] { get; }
    }
}