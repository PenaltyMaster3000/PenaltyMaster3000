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
        /// <summary>
        /// Creates all baseGesture objects needed for the penaly master 
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
            PostureTwoHandsDown postureTwoHandsDown = new PostureTwoHandsDown();
            PostureTwoHandsUp postureTwoHandsUp = new PostureTwoHandsUp();

            // Gesture
            SoccerShootGesture soccerShootGesture = new SoccerShootGesture();

            BaseGesture[] gestures = new BaseGesture[7];
            gestures[0] = postureHandUpLeft;
            gestures[1] = postureHandUpRight;
            gestures[2] = postureHandDownLeft;
            gestures[3] = postureHandDownRight;
            gestures[4] = postureTwoHandsDown;
            gestures[5] = postureTwoHandsUp;
            gestures[6] = soccerShootGesture;

            return gestures;
        }
    }
}
