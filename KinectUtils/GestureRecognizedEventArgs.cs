using Microsoft.Kinect;

namespace KinectUtils
{
    /// <summary>
    /// The gesture recognized event args.
    /// </summary>
    public class GestureRecognizedEventArgs
    {
        /// <summary>
        /// The gesture name.
        /// </summary>
        public string GestureName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GestureRecognizedEventArgs"/> class.
        /// </summary>
        /// <param name="gestureName"></param>
        /// <param name="body"></param>
        public GestureRecognizedEventArgs(string gestureName)
        {
            GestureName = gestureName;
        }
    }
}