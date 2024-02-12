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

        private Visibility questionPointTopRightVisibility;
        public Visibility QuestionPointTopRightVisibility
        {
            get => questionPointTopRightVisibility;
            set => SetProperty(ref questionPointTopRightVisibility, value);
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

        private Visibility questionPointTopLeftVisibility;
        public Visibility QuestionPointTopLeftVisibility
        {
            get => questionPointTopLeftVisibility;
            set => SetProperty(ref questionPointTopLeftVisibility, value);
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

        private Visibility questionPointTopMiddleVisibility;
        public Visibility QuestionPointTopMiddleVisibility
        {
            get => questionPointTopMiddleVisibility;
            set => SetProperty(ref questionPointTopMiddleVisibility, value);
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

        private Visibility questionPointDownMiddleVisibility;
        public Visibility QuestionPointDownMiddleVisibility
        {
            get => questionPointDownMiddleVisibility;
            set => SetProperty(ref questionPointDownMiddleVisibility, value);
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

        private Visibility questionPointDownRightVisibility;
        public Visibility QuestionPointDownRightVisibility
        {
            get => questionPointDownRightVisibility;
            set => SetProperty(ref questionPointDownRightVisibility, value);
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

        private Visibility questionPointDownLeftVisibility;
        public Visibility QuestionPointDownLeftVisibility
        {
            get => questionPointDownLeftVisibility;
            set => SetProperty(ref questionPointDownLeftVisibility, value);
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
        /// Checks if two elements on the same position are visible.
        /// </summary>
        /// <returns></returns>
        public bool GetResult()
        {
            return AreElementsVisible(BallTopRightVisibility, GoalTopRightVisibility) ||
                   AreElementsVisible(BallTopMiddleVisibility, GoalTopMiddleVisibility) ||
                   AreElementsVisible(BallTopLeftVisibility, GoalTopLeftVisibility) ||
                   AreElementsVisible(BallDownRightVisibility, GoalDownRightVisibility) ||
                   AreElementsVisible(BallDownMiddleVisibility, GoalDownMiddleVisibility) ||
                   AreElementsVisible(BallDownLeftVisibility, GoalDownLeftVisibility);
        }

        // Méthode utilitaire pour vérifier si deux éléments sont visibles
        private bool AreElementsVisible(Visibility element1, Visibility element2)
        {
            return element1 == Visibility.Visible && element2 == Visibility.Visible;
        }

        /// <summary>
        /// Update the view with the question point position.
        /// </summary>
        /// <param name="gesturePosition"></param>
        public void SetQuestionPoint(string gesturePosition)
        {
            switch (gesturePosition)
            {
                case "HandUpRight":
                    QuestionPointTopRightVisibility = Visibility.Visible;
                    break;

                case "HandUpLeft":
                    QuestionPointTopLeftVisibility = Visibility.Visible;
                    break;

                case "HandDownRight":
                    QuestionPointDownRightVisibility = Visibility.Visible;
                    break;

                case "HandDownLeft":
                    QuestionPointDownLeftVisibility = Visibility.Visible;
                    break;

                default: return;
            }
        }

        /// <summary>
        /// Update the view with the latest shot and defense.
        /// </summary>
        /// <param name="shotPosition"></param>
        /// <param name="defensePosition"></param>
        public async Task SetResult(string shotPosition, string defensePosition)
        {

            updateBall(shotPosition);

            // Delay to add some drama to the game
            await Task.Delay(500);

            updateKeeper(defensePosition);

            await Task.Delay(3000);
        }

        /// <summary>
        /// Udaptes the ball position based on the gesture name.
        /// </summary>
        /// <param name="shotPosition"></param>
        private void updateBall(string shotPosition)
        {
            if(StarterBall == Visibility.Visible) 
            {
                StarterBall = Visibility.Hidden;
            }

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
            if(StarterGoal == Visibility.Visible)
            {
                StarterGoal = Visibility.Hidden;
            }

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
                    GoalDownLeftVisibility = Visibility.Visible;
                    break;

                default: return;
            }
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
            QuestionPointTopRightVisibility = Visibility.Hidden;
            BallTopMiddleVisibility = Visibility.Hidden;
            GoalTopMiddleVisibility = Visibility.Hidden;
            QuestionPointTopMiddleVisibility = Visibility.Hidden;
            BallTopLeftVisibility = Visibility.Hidden;
            GoalTopLeftVisibility = Visibility.Hidden;
            QuestionPointTopLeftVisibility = Visibility.Hidden;
            BallDownRightVisibility = Visibility.Hidden;
            GoalDownRightVisibility = Visibility.Hidden;
            QuestionPointDownRightVisibility = Visibility.Hidden;
            BallDownMiddleVisibility = Visibility.Hidden;
            GoalDownMiddleVisibility = Visibility.Hidden;
            QuestionPointDownMiddleVisibility = Visibility.Hidden;
            BallDownLeftVisibility = Visibility.Hidden;
            GoalDownLeftVisibility = Visibility.Hidden;
            QuestionPointDownLeftVisibility = Visibility.Hidden;
        }

        public void HideQuestionPoint()
        {
            QuestionPointTopRightVisibility = Visibility.Hidden;
            QuestionPointTopMiddleVisibility = Visibility.Hidden;
            QuestionPointTopLeftVisibility= Visibility.Hidden;
            QuestionPointDownRightVisibility= Visibility.Hidden;
            QuestionPointDownMiddleVisibility= Visibility.Hidden;
            QuestionPointDownLeftVisibility= Visibility.Hidden;
        }
    }
}
