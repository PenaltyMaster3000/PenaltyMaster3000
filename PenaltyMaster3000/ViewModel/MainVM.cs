using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinectUtils;
using MyGestureBank;
using PenaltyMaster3000.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PenaltyMaster3000.ViewModel
{
    public class MainVM : ObservableObject
    {
        public IGestureFactory GestureFactory { get; set; }

        public String ShotHolder {  get; set; }
        public String DefenseHolder {  get; set; }

        private DispatcherTimer goalTimer = new DispatcherTimer();

        private DispatcherTimer refereeTimer = new DispatcherTimer();

        private DispatcherTimer scoreBoardTimer = new DispatcherTimer();

        private Random random = new Random();

        private bool image1Active = true;

        private int player1Score = 0;
        
        private int player2Score = 0;

        private bool isShootCompleted;

        private bool isSaveCompleted;

        private bool isResultCompleted;

        private bool isPlayer1Goalkeeper = false;

        private int numberOfShoots = 0;

        private string actionText;
        public string ActionText
        {
            get => actionText;
            set => SetProperty(ref actionText, value);
        }

        private Visibility actionTextVisibility;
        public Visibility ActionTextVisibility
        {
            get => actionTextVisibility;
            set => SetProperty(ref actionTextVisibility, value);
        }

        private Visibility whistleImageVisibility;
        public Visibility WhistleImageVisibility
        {
            get => whistleImageVisibility;
            set => SetProperty(ref whistleImageVisibility, value);
        }

        private Visibility scoreBoardVisibility;
        public Visibility ScoreBoardVisibility
        {
            get => scoreBoardVisibility;
            set => SetProperty(ref scoreBoardVisibility, value);
        }

        private Visibility finalResultVisibility;
        public Visibility FinalResultVisibility
        {
            get => finalResultVisibility;
            set => SetProperty(ref finalResultVisibility, value);
        }

        /// <summary>
        /// The goal's visibility manager.
        /// </summary>
        private VisibilityManager vsMgr;
        public VisibilityManager VsMgr
        {
            get => vsMgr;
            set => SetProperty(ref vsMgr, value);
        }

        // PLAYER TEXTS
        private string player1ScoreText;
        public string Player1ScoreText
        {
            get => player1ScoreText;
            set => SetProperty(ref player1ScoreText, value);
        }

        private string player2ScoreText;
        public string Player2ScoreText
        {
            get => player2ScoreText;
            set => SetProperty(ref player2ScoreText, value);
        }

        private string finalWinnerText;
        public string FinalWinnerText
        {
            get => finalWinnerText;
            set => SetProperty(ref finalWinnerText, value);
        }

        // ---
                            
        public BitmapImage CurrentImageSource
        {
            get
            {
                string imagePath = image1Active ? "/Images/goalkeeper_starting1.png" : "/Images/goalkeeper_starting2.png";
                return new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }
        }

        public bool IsShootCompleted
        {
            get { return isShootCompleted; }
            set
            {
                if (SetProperty(ref isShootCompleted, value))
                {
                    if (value)
                    {
                        SaveCommand.Execute(null);
                    }
                }
            }
        }

        public bool IsSaveCompleted
        {
            get { return isSaveCompleted; }
            set
            {
                if (SetProperty(ref isSaveCompleted, value))
                {
                    if (value)
                    {
                        ResultCommand.Execute(null);
                    }
                }
            }
        }

        public bool IsResultCompleted
        {
            get { return isResultCompleted; }
            set
            {
                if (SetProperty(ref isResultCompleted, value))
                {
                    if (value)
                    {
                        ShootCommand.Execute(null);
                    }
                }
            }
        }

        // ---

        
        public ICommand ShootCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand ResultCommand { get; private set; }
        
        public MainVM() 
        {
            this.GestureFactory = new PenaltyMasterGestureFactory();

            VsMgr = new VisibilityManager();

            goalTimer.Interval = TimeSpan.FromSeconds(1);
            goalTimer.Tick += GoalTimer_Tick;
            goalTimer.Start();

            ActionTextVisibility = Visibility.Hidden;
            WhistleImageVisibility = Visibility.Hidden;
            FinalResultVisibility = Visibility.Hidden;

            ShootCommand = new RelayCommand(Shoot);
            SaveCommand = new RelayCommand(Save);
            ResultCommand = new RelayCommand(Result);

            Shoot();
        }

        private void GoalTimer_Tick(object sender, EventArgs e)
        {
            image1Active = !image1Active;
            OnPropertyChanged(nameof(CurrentImageSource));
        }

        /// <summary>
        /// Hides the action text when it expires. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefereeTimer_Tick(object sender, EventArgs e)
        {
            ActionTextVisibility = Visibility.Hidden;
            WhistleImageVisibility = Visibility.Hidden;

            // Arrêter le timer
            refereeTimer.Stop();
        }

        private void ScoreBoardTimer_Tick(object sender, EventArgs e)
        {
            // Rendre le texte invisible lorsque le timer expire
            ScoreBoardVisibility = Visibility.Hidden;
            scoreBoardTimer.Stop();
        }

        private async void Shoot()
        {
            StartupVisibility();

            // Après que le timer a expiré, marquez le tir comme terminé
            IsShootCompleted = true;

            // Générez un index aléatoire pour choisir l'élément Goal à rendre visible
            //int randomIndex = random.Next(goalVisibilities.Count);

            // Let the player choose a angle to shoot
            await DisplayActionText("Shooter's turn. Get ready !", 3);
            await DisplayActionText("Choose an angle to shoot.", 5);

            // Read a gesture for 5 seconds
            // Save the shot on the ShotHolder attribute
            ShotHolder = await ReadAGesture(2);

            // IsShootCompleted = false;
            // Call the save method
            Save();
        }

        private async void Save()
        {
            await DisplayActionText("GoalKeeper's turn. Get ready !", 3);
            await DisplayActionText("Choose an angle to defend.", 5);

            DefenseHolder = await ReadAGesture(2);

            // Arrêter le timer du déplacement automatique du Goal
            goalTimer.Stop();

            // IsSaveCompleted = false;

            Result();
        }

        private async void Result()
        {
            // Update the view with shot and defense.
            await VsMgr.SetResult(ShotHolder, DefenseHolder);

            // get the result (goal or not goal).
            bool isBallAndGoalVisible = VsMgr.GetResult();

            // Update the score 

            // Vérifiez si toutes les paires d'éléments Ball et Goal sont visibles
            if (!isPlayer1Goalkeeper && !isBallAndGoalVisible)
            {
                // Une paire d'éléments est visible, incrémentation du score du Shooter
                player1Score++;
            }
            else if (isPlayer1Goalkeeper && !isBallAndGoalVisible)
            {
                // Aucune paire d'éléments est visible, incrémentation du score du Goalkeeper
                player2Score++;
            }

            Player1ScoreText = player1Score.ToString();
            Player2ScoreText = player2Score.ToString();

            ScoreBoardVisibility = Visibility.Visible;

            // Arrêter le timer précédent s'il était en cours
            if (scoreBoardTimer != null && scoreBoardTimer.IsEnabled)
            {
                scoreBoardTimer.Stop();
            }

            // Démarrer un nouveau timer pour rendre le texte invisible après 2 secondes
            scoreBoardTimer = new DispatcherTimer();
            scoreBoardTimer.Interval = TimeSpan.FromSeconds(2);
            scoreBoardTimer.Tick += (sender, args) =>
            {
                ScoreBoardTimer_Tick(sender, args);

                numberOfShoots++;
                if (numberOfShoots >= 10 && numberOfShoots % 2 == 0 && player1Score != player2Score)
                {
                    GameEnded();
                }
                else
                {
                    // Après que le timer a expiré, marquez l'affichage du résultat comme terminé
                    IsResultCompleted = true;
                }
            };
            scoreBoardTimer.Start();

            isPlayer1Goalkeeper = !isPlayer1Goalkeeper;

            StartupVisibility();
            goalTimer.Start();

            IsResultCompleted = false;
        }

        private void GameEnded()
        {
            if(player1Score > player2Score)
            {
                FinalWinnerText = "Player 1 wins the game !";
            }
            else
            {
                FinalWinnerText = "Player 2 wins the game !";
            }
            FinalResultVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Sets the starting visibility for the game.
        /// </summary>
        private void StartupVisibility()
        {
            VsMgr.GameStartedVisibility();
        }

        // new stuff

        /// <summary>
        /// Display an Action text.
        /// </summary>
        /// <param name="textToBeDisplayed"></param>
        /// <param name="timeToBeDisplayedInSeconds"></param>
        private Task DisplayActionText(string textToBeDisplayed, int timeToBeDisplayedInSeconds)
        {
            var tcs = new TaskCompletionSource<bool>();

            // Set text to be displayed
            ActionText = textToBeDisplayed;
            ActionTextVisibility = Visibility.Visible;

            // Hide scoreboard
            ScoreBoardVisibility = Visibility.Hidden;

            // Arrêter le timer précédent s'il était en cours
            if (refereeTimer != null && refereeTimer.IsEnabled)
            {
                refereeTimer.Stop();
            }

            // Démarrer un nouveau timer pour rendre le texte invisible après x secondes
            refereeTimer = new DispatcherTimer();
            refereeTimer.Interval = TimeSpan.FromSeconds(timeToBeDisplayedInSeconds);
            refereeTimer.Tick += (sender, args) =>
            {
                RefereeTimer_Tick(sender, args);
                tcs.SetResult(true);
            };

            refereeTimer.Start();

            return tcs.Task;
        }
        /*
         * Use the gesture manager to read a gesture
         */
        private async Task<string> ReadAGesture(int ReadTimeInSeconds)
        {
            string gestureRead = "";

            // load all 
            GestureManager.AddGestures(this.GestureFactory);

            /*// subscirbe to the OnGestureRecognized event 
            foreach (var gesture in GestureManager.KnownGestures)
            {
                gesture.GestureRecognized += (sender, args) =>
                {
                    // If new gesture read, replace th gesture name
                    if(gestureRead != args.GestureName)
                    {
                        // [TODO?] Display gesture on screen
                        gestureRead = args.GestureName;
                        */
                        gestureRead = "HandUpRight"; //TEST [TO REPLACE BY gestureRead = args.GestureName;]
                        VsMgr.SetQuestionPoint(gestureRead);
                        /*
                    }
                };
            }

            // Read frames for ReadTimeSeconds
            GestureManager.StartAcquiringFrames(GestureManager.KinectManager);
            await Task.Delay(ReadTimeInSeconds*1000);

            // Stop reading and unsub from the GestureRecognized event to prevent memory leaks
            GestureManager.StopAcquiringFrame(GestureManager.KinectManager);
            foreach (var gesture in GestureManager.KnownGestures)
            {
                gesture.GestureRecognized = null;
            }*/

            // return gestureRead;
            return "HandUpRight";
        }
    }
}
