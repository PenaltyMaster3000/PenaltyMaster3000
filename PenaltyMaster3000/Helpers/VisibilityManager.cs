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
    }
}
