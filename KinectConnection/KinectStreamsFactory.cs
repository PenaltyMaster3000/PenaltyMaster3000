using KinectConnection.enums;
using System;
using System.Collections.Generic;

namespace KinectConnection
{
    /// <summary>
    /// The kinect streams factory.
    /// </summary>
    public class KinectStreamsFactory
    {
        /// <summary>
        /// The stream factory dictionary.
        /// Maps a KinectStream to a function that creates the proper stream.
        /// </summary>
        private Dictionary<KinectStreams, Func<KinectStream>> streamFactory;

        public KinectStreamsFactory(KinectManager kinect)
        {
            streamFactory = new Dictionary<KinectStreams, Func<KinectStream>>
        {
            { KinectStreams.Color, () => new ColorImageStream() },
            // Other streams ...
        };
        }

        /// <summary>
        /// Indexer to get a KinectStream from the factory.
        /// This allows to create a stream from a KinectStreams enum.
        /// </summary>
        /// <param name="stream">The kinect stream.</param>
        /// <returns>The kinect stream instance.</returns>
        public KinectStream this[KinectStreams stream]
        {
            get { return streamFactory[stream](); }
        }
    }
}
