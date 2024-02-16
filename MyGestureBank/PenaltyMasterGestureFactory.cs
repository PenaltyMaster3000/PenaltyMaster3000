using MyGestureBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectUtils
{
    /// <summary>
    /// The penalty master gesture factory.
    /// </summary>
    public class PenaltyMasterGestureFactory : IGestureFactory
    {
        private Dictionary<string, BaseGesture> gestures = new Dictionary<string, BaseGesture>();

        /// <summary>
        /// Creates all baseGesture objects needed for the penalty master 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<BaseGesture> CreateGestures()
        {
            // Postures
            PostureHandUpRight postureHandUpRight = new PostureHandUpRight();
            PostureHandUpLeft postureHandUpLeft = new PostureHandUpLeft();
            PostureHandDownLeft postureHandDownLeft = new PostureHandDownLeft();
            PostureHandDownRight postureHandDownRight = new PostureHandDownRight();
            //PostureTwoHandsDown postureTwoHandsDown = new PostureTwoHandsDown();
            //PostureTwoHandsUp postureTwoHandsUp = new PostureTwoHandsUp();

            // Gesture
            SoccerShootGesture soccerShootGesture = new SoccerShootGesture();

            // Add gestures to the dictionary
            gestures.Add("PostureHandUpRight", postureHandUpRight);
            gestures.Add("PostureHandUpLeft", postureHandUpLeft);
            gestures.Add("PostureHandDownLeft", postureHandDownLeft);
            gestures.Add("PostureHandDownRight", postureHandDownRight);
            //gestures.Add("PostureTwoHandsDown", postureTwoHandsDown);
            //gestures.Add("PostureTwoHandsUp", postureTwoHandsUp);
            gestures.Add("SoccerShootGesture", soccerShootGesture);

            return gestures.Values;
        }

        // Indexer to get a gesture by its name
        public BaseGesture this[string name]
        {
            get
            {
                if (gestures.ContainsKey(name))
                {
                    return gestures[name];
                }
                else
                {
                    throw new KeyNotFoundException($"Gesture with name {name} not found.");
                }
            }
        }
    }
}