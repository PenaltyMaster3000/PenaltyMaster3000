using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PenaltyMaster3000.Helpers
{
    // [TODO] Change 'Goal' to 'Keeper' to avoid confusion.

    /// <summary>
    /// Manages the visibility of the goal's four corners + middle zone.
    /// </summary>
    public class VisibilityManager : ObservableObject
    {
        // Top right
        private Visibility ballTopRightVisibility;
        public Visibility BallTopRightVisibility
        {
            get => ballTopRightVisibility;
            set => SetProperty(ref ballTopRightVisibility, value);
        }

        private Visibility goalTopRightVisibility;
        public Visibility GoalTopRightVisibility
        {
            get => goalTopRightVisibility;
            set => SetProperty(ref goalTopRightVisibility, value);
        }

        // Top Left
        private Visibility ballTopLeftVisibility;
        public Visibility BallTopLeftVisibility
        {
            get => ballTopLeftVisibility;
            set => SetProperty(ref ballTopLeftVisibility, value);
        }

        private Visibility goalTopLeftVisibility;
        public Visibility GoalTopLeftVisibility
        {
            get => goalTopLeftVisibility;
            set => SetProperty(ref goalTopLeftVisibility, value);
        }
        // ---

        // Middle
        private Visibility ballTopMiddleVisibility;
        public Visibility BallTopMiddleVisibility
        {
            get => ballTopMiddleVisibility;
            set => SetProperty(ref ballTopMiddleVisibility, value);
        }

        private Visibility goalTopMiddleVisibility;
        public Visibility GoalTopMiddleVisibility
        {
            get => goalTopMiddleVisibility;
            set => SetProperty(ref goalTopMiddleVisibility, value);
        }

        // Down middle
        private Visibility ballDownMiddleVisibility;
        public Visibility BallDownMiddleVisibility
        {
            get => ballDownMiddleVisibility;
            set => SetProperty(ref ballDownMiddleVisibility, value);
        }

        private Visibility goalDownMiddleVisibility;
        public Visibility GoalDownMiddleVisibility
        {
            get => goalDownMiddleVisibility;
            set => SetProperty(ref goalDownMiddleVisibility, value);
        }
        // ---

        // Down right
        private Visibility ballDownRightVisibility;
        public Visibility BallDownRightVisibility
        {
            get => ballDownRightVisibility;
            set => SetProperty(ref ballDownRightVisibility, value);
        }

        private Visibility goalDownRightVisibility;
        public Visibility GoalDownRightVisibility
        {
            get => goalDownRightVisibility;
            set => SetProperty(ref goalDownRightVisibility, value);
        }

        // Down left
        private Visibility ballDownLeftVisibility;
        public Visibility BallDownLeftVisibility
        {
            get => ballDownLeftVisibility;
            set => SetProperty(ref ballDownLeftVisibility, value);
        }

        private Visibility goalDownLeftVisibility;
        public Visibility GoalDownLeftVisibility
        {
            get => goalDownLeftVisibility;
            set => SetProperty(ref goalDownLeftVisibility, value);
        }
        /// --- 


        /// Starting ball and GoalKeeper
        private Visibility starterBall;
        public Visibility StarterBall
        {
            get => starterBall;
            set => SetProperty(ref starterBall, value);
        }

        private Visibility starterGoal;
        public Visibility StarterGoal
        {
            get => starterGoal;
            set => SetProperty(ref starterGoal, value);
        }

        /// <summary>
        /// Sets the visibility of the elements for the start of the game.
        /// </summary>
        public void GameStartedVisibility()
        {
            // Starter attributes visible
            StarterBall = Visibility.Visible;
            StarterGoal = Visibility.Visible;

            // Rest should be hidden
            BallTopRightVisibility = Visibility.Hidden;
            GoalTopRightVisibility = Visibility.Hidden;
            BallTopMiddleVisibility = Visibility.Hidden;
            GoalTopMiddleVisibility = Visibility.Hidden;
            BallTopLeftVisibility = Visibility.Hidden;
            GoalTopLeftVisibility = Visibility.Hidden;
            BallDownRightVisibility = Visibility.Hidden;
            GoalDownRightVisibility = Visibility.Hidden;
            BallDownMiddleVisibility = Visibility.Hidden;
            GoalDownMiddleVisibility = Visibility.Hidden;
            BallDownLeftVisibility = Visibility.Hidden;
            GoalDownLeftVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// Update the view with the latest shot and defense.
        /// </summary>
        /// <param name="shotPosition"></param>
        /// <param name="defensePosition"></param>
        public void SetResult(string shotPosition, string defensePosition)
        {

            updateBall(shotPosition);
            updateKeeper(defensePosition);
        }

        /// <summary>
        /// Udaptes the ball position based on the gesture name.
        /// </summary>
        /// <param name="shotPosition"></param>
        private void updateBall(string shotPosition)
        {
            switch (shotPosition)
            {
                case "HandUpRight":
                    BallTopRightVisibility = Visibility.Visible;
                    break;

                case "HandUpLeft":
                    BallTopLeftVisibility = Visibility.Visible;
                    break;

                case "HandDownRight":
                    BallDownRightVisibility = Visibility.Visible;
                    break;

                case "HandDownLeft":
                    BallTopRightVisibility = Visibility.Visible;
                    break;

                default: return;
            }
        }

        /// <summary>
        /// Updates the goalkeeper position based on the gesture name.
        /// </summary>
        /// <param name="defensePosition"></param>
        private void updateKeeper(string defensePosition)
        {
            switch (defensePosition)
            {
                case "HandUpRight":
                    GoalTopRightVisibility = Visibility.Visible;
                    break;

                case "HandUpLeft":
                    GoalTopLeftVisibility = Visibility.Visible;
                    break;

                case "HandDownRight":
                    GoalDownRightVisibility = Visibility.Visible;
                    break;

                case "HandDownLeft":
                    GoalTopRightVisibility = Visibility.Visible;
                    break;

                default: return;
            }
        }
    }
}
