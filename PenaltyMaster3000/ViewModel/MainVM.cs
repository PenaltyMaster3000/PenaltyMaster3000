using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KinectUtils;
using MyGestureBank;
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

        public string ActionText { get; private set; }

        public Visibility ActionTextVisibility { get; private set; }

        public Visibility WhistleImageVisibility { get; private set; }

        public Visibility ScoreBoardVisibility { get; private set; }

        public Visibility FinalResultVisibility { get; private set; }

        public Visibility BallStartingVisibility { get; private set; }
        public Visibility GoalStartingVisibility { get; private set; }

        public Visibility BallTopRightVisibility { get; private set; }
        public Visibility GoalTopRightVisibility { get; private set; }

        public Visibility BallTopMiddleVisibility { get; private set; }
        public Visibility GoalTopMiddleVisibility { get; private set; }
        
        public Visibility BallTopLeftVisibility { get; private set; }
        public Visibility GoalTopLeftVisibility { get; private set; }
        
        public Visibility BallDownRightVisibility { get; private set; }
        public Visibility GoalDownRightVisibility { get; private set; }

        public Visibility BallDownMiddleVisibility { get; private set; }
        public Visibility GoalDownMiddleVisibility { get; private set; }

        public Visibility BallDownLeftVisibility { get; private set; }
        public Visibility GoalDownLeftVisibility { get; private set; }

        public string Player1ScoreText { get; private set; }

        public string Player2ScoreText { get; private set; }

        public string FinalWinnerText { get; private set; }

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

        private void RefereeTimer_Tick(object sender, EventArgs e)
        {
            // Rendre le texte invisible lorsque le timer expire
            ActionTextVisibility = Visibility.Hidden;
            WhistleImageVisibility = Visibility.Hidden;
            OnPropertyChanged(nameof(ActionTextVisibility));
            OnPropertyChanged(nameof(WhistleImageVisibility));

            // Arrêter le timer
            refereeTimer.Stop();
        }

        private void ScoreBoardTimer_Tick(object sender, EventArgs e)
        {
            // Rendre le texte invisible lorsque le timer expire
            ScoreBoardVisibility = Visibility.Hidden;
            OnPropertyChanged(nameof(ScoreBoardVisibility));

            // Arrêter le timer
            scoreBoardTimer.Stop();
        }

        private void Shoot()
        {
            StartupVisibility();

            displayTextOnScreen(2, "Get Ready to Shoot !");
            // Après que le timer a expiré, marquez le tir comme terminé
            IsShootCompleted = true;
            OnPropertyChanged(nameof(IsShootCompleted));


            // Liste des propriétés de visibilité des éléments Goal
            List<Visibility> goalVisibilities = new List<Visibility>
            {
                GoalTopRightVisibility, GoalTopMiddleVisibility, GoalTopLeftVisibility,
                GoalDownRightVisibility, GoalDownMiddleVisibility, GoalDownLeftVisibility
            };

            // Générez un index aléatoire pour choisir l'élément Goal à rendre visible
            //int randomIndex = random.Next(goalVisibilities.Count);

            // Let the player choose a angle to shoot
            displayTextOnScreen(2, "Choose an angle to shoot.");
            

            // Définissez la visibilité de l'élément Goal choisi sur Visible
            //goalVisibilities[randomIndex] = Visibility.Visible;

            BallStartingVisibility = Visibility.Hidden;
            GoalStartingVisibility = Visibility.Hidden;
            OnPropertyChanged(nameof(BallStartingVisibility));
            OnPropertyChanged(nameof(GoalStartingVisibility));

            GoalTopRightVisibility = goalVisibilities[0];
            GoalTopMiddleVisibility = goalVisibilities[1];
            GoalTopLeftVisibility = goalVisibilities[2];
            GoalDownRightVisibility = goalVisibilities[3];
            GoalDownMiddleVisibility = goalVisibilities[4];
            GoalDownLeftVisibility = goalVisibilities[5];

            // A DEPLACER POUR APRES
            OnPropertyChanged(nameof(GoalTopRightVisibility));
            OnPropertyChanged(nameof(GoalTopMiddleVisibility));
            OnPropertyChanged(nameof(GoalTopLeftVisibility));
            OnPropertyChanged(nameof(GoalDownRightVisibility));
            OnPropertyChanged(nameof(GoalDownMiddleVisibility));
            OnPropertyChanged(nameof(GoalDownLeftVisibility));
            
            IsShootCompleted = false;
            OnPropertyChanged(nameof(IsShootCompleted));
        }

        private void Save()
        {
            ActionText = "Save !";
            ActionTextVisibility = Visibility.Visible;
            WhistleImageVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(ActionText));
            OnPropertyChanged(nameof(ActionTextVisibility));
            OnPropertyChanged(nameof(WhistleImageVisibility));

            // Arrêter le timer précédent s'il était en cours
            if (refereeTimer != null && refereeTimer.IsEnabled)
            {
                refereeTimer.Stop();
            }

            // Démarrer un nouveau timer pour rendre le texte invisible après 2 secondes
            refereeTimer = new DispatcherTimer();
            refereeTimer.Interval = TimeSpan.FromSeconds(2);
            refereeTimer.Tick += (sender, args) =>
            {
                RefereeTimer_Tick(sender, args);
                // Après que le timer a expiré, marquez l'arrêt comme terminé
                IsSaveCompleted = true;
                OnPropertyChanged(nameof(IsSaveCompleted));
            };
            refereeTimer.Start();

            // Arrêter le timer du déplacement automatique du Goal
            goalTimer.Stop();

            // Liste des propriétés de visibilité des éléments Ball [TEMPORAIRE !]
            List<Visibility> ballVisibilities = new List<Visibility>
            {
                BallTopRightVisibility, BallTopMiddleVisibility, BallTopLeftVisibility,
                BallDownRightVisibility, BallDownMiddleVisibility, BallDownLeftVisibility
            };

            // Générez un index aléatoire pour choisir l'élément Goal à rendre visible
            int randomIndex = random.Next(ballVisibilities.Count);

            // Définissez la visibilité de l'élément Goal choisi sur Visible
            ballVisibilities[randomIndex] = Visibility.Visible;

            BallStartingVisibility = Visibility.Hidden;
            GoalStartingVisibility = Visibility.Hidden;
            OnPropertyChanged(nameof(BallStartingVisibility));
            OnPropertyChanged(nameof(GoalStartingVisibility));

            BallTopRightVisibility = ballVisibilities[0];
            BallTopMiddleVisibility = ballVisibilities[1];
            BallTopLeftVisibility = ballVisibilities[2];
            BallDownRightVisibility = ballVisibilities[3];
            BallDownMiddleVisibility = ballVisibilities[4];
            BallDownLeftVisibility = ballVisibilities[5];

            OnPropertyChanged(nameof(BallTopRightVisibility));
            OnPropertyChanged(nameof(BallTopMiddleVisibility));
            OnPropertyChanged(nameof(BallTopLeftVisibility));
            OnPropertyChanged(nameof(BallDownRightVisibility));
            OnPropertyChanged(nameof(BallDownMiddleVisibility));
            OnPropertyChanged(nameof(BallDownLeftVisibility));

            IsSaveCompleted = false;
            OnPropertyChanged(nameof(IsSaveCompleted));
        }

        private void Result()
        {   
            bool isBallAndGoalVisible = AreElementsVisible(BallTopRightVisibility, GoalTopRightVisibility) ||
                                        AreElementsVisible(BallTopMiddleVisibility, GoalTopMiddleVisibility) ||
                                        AreElementsVisible(BallTopLeftVisibility, GoalTopLeftVisibility) ||
                                        AreElementsVisible(BallDownRightVisibility, GoalDownRightVisibility) ||
                                        AreElementsVisible(BallDownMiddleVisibility, GoalDownMiddleVisibility) ||
                                        AreElementsVisible(BallDownLeftVisibility, GoalDownLeftVisibility);

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
            OnPropertyChanged(nameof(Player1ScoreText));
            OnPropertyChanged(nameof(Player2ScoreText));

            ScoreBoardVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(ScoreBoardVisibility));

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
                    OnPropertyChanged(nameof(IsResultCompleted));
                }
            };
            scoreBoardTimer.Start();

            isPlayer1Goalkeeper = !isPlayer1Goalkeeper;

            StartupVisibility();
            goalTimer.Start();

            IsResultCompleted = false;
            OnPropertyChanged(nameof(IsResultCompleted));
        }

        // Méthode utilitaire pour vérifier si deux éléments sont visibles
        private bool AreElementsVisible(Visibility element1, Visibility element2)
        {
            return element1 == Visibility.Visible && element2 == Visibility.Visible;
        }

        private void StartupVisibility()
        {
            BallStartingVisibility = Visibility.Visible;
            GoalStartingVisibility = Visibility.Visible;
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

            OnPropertyChanged(nameof(BallStartingVisibility));
            OnPropertyChanged(nameof(GoalStartingVisibility));
            OnPropertyChanged(nameof(BallTopRightVisibility));
            OnPropertyChanged(nameof(GoalTopRightVisibility));
            OnPropertyChanged(nameof(BallTopMiddleVisibility));
            OnPropertyChanged(nameof(GoalTopMiddleVisibility));
            OnPropertyChanged(nameof(BallTopLeftVisibility));
            OnPropertyChanged(nameof(GoalTopLeftVisibility));
            OnPropertyChanged(nameof(BallDownRightVisibility));
            OnPropertyChanged(nameof(GoalDownRightVisibility));
            OnPropertyChanged(nameof(BallDownMiddleVisibility));
            OnPropertyChanged(nameof(GoalDownMiddleVisibility));
            OnPropertyChanged(nameof(BallDownLeftVisibility));
            OnPropertyChanged(nameof(GoalDownLeftVisibility));
        }

        private void GameEnded()
        {
            if(player1Score > player2Score)
            {
                FinalWinnerText = "Player 1 wins the game !";
                OnPropertyChanged(nameof(FinalWinnerText));
            }
            else
            {
                FinalWinnerText = "Player 2 wins the game !";
                OnPropertyChanged(nameof(FinalWinnerText));
            }
            FinalResultVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(FinalResultVisibility));
        }

        // new stuff

        private void displayTextOnScreen(int timeToBeDisplayedInSeconds, string textToBeDisplayed)
        {
            ActionText = textToBeDisplayed;
            ActionTextVisibility = Visibility.Visible;
            WhistleImageVisibility = Visibility.Visible;
            ScoreBoardVisibility = Visibility.Hidden;
            OnPropertyChanged(nameof(ActionText));
            OnPropertyChanged(nameof(ActionTextVisibility));
            OnPropertyChanged(nameof(WhistleImageVisibility));
            OnPropertyChanged(nameof(ScoreBoardVisibility));

            // Arrêter le timer précédent s'il était en cours
            if (refereeTimer != null && refereeTimer.IsEnabled)
            {
                refereeTimer.Stop();
            }

            // Démarrer un nouveau timer pour rendre le texte invisible après 2 secondes
            refereeTimer = new DispatcherTimer();
            refereeTimer.Interval = TimeSpan.FromSeconds(timeToBeDisplayedInSeconds);
            refereeTimer.Tick += (sender, args) =>
            {
                RefereeTimer_Tick(sender, args);
            };

            refereeTimer.Start();
        }

        /*
         * Use the gesture manager to read a gesture
         */
        private async Task<string> ReadAGesture(int ReadTimeInSeconds)
        {
            string gestureRead = "";

            // load all 
            GestureManager.AddGestures(this.GestureFactory);

            // subscirbe to the OnGestureRecognized event 
            foreach (var gesture in GestureManager.KnownGestures)
            {
                gesture.GestureRecognized += (sender, args) =>
                {
                    // If new gesture read, replace th gesture name
                    if(gestureRead != args.GestureName)
                    {
                        gestureRead = args.GestureName;
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
            }

            return gestureRead;
        }
    }
}
