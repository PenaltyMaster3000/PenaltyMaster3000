using System;
using System.Windows.Media;

namespace KinectConnection
{
    /// <summary>
    /// The depth image stream.
    /// </summary>
    public class DepthImageStream : KinectStream
    {
        public override ImageSource Source => throw new NotImplementedException();

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
