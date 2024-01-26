using System;
using System.Windows.Media;

namespace KinectConnection
{
    /// <summary>
    /// The infrared image stream.
    /// </summary>
    public class InfraredImageStream : KinectStream
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
