using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private DispatcherTimer goalTimer = new DispatcherTimer();

        private DispatcherTimer refereeTimer = new DispatcherTimer();

        private bool image1Active = true;

        public string ActionText { get; private set; }

        public Visibility ActionTextVisibility { get; private set; }

        public Visibility WhistleImageVisibility { get; private set; }

        public BitmapImage CurrentImageSource
        {
            get
            {
                string imagePath = image1Active ? "/Images/goalkeeper_starting1.png" : "/Images/goalkeeper_starting2.png";
                return new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }
        }

        public ICommand ShootCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand ResultCommand { get; private set; }

        public MainVM() 
        {
            goalTimer.Interval = TimeSpan.FromSeconds(1);
            goalTimer.Tick += GoalTimer_Tick;
            goalTimer.Start();
            ActionTextVisibility = Visibility.Hidden;
            WhistleImageVisibility = Visibility.Hidden;
            ShootCommand = new RelayCommand(Shoot);
            SaveCommand = new RelayCommand(Save);
            ResultCommand = new RelayCommand(Result);
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

        private void Shoot()
        {
            ActionText = "Shoot !";
            ActionTextVisibility = Visibility.Visible;
            WhistleImageVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(ActionTextVisibility));
            OnPropertyChanged(nameof(WhistleImageVisibility));

            // Arrêter le timer précédent s'il était en cours
            if (refereeTimer != null && refereeTimer.IsEnabled)
            {
                refereeTimer.Stop();
            }

            // Démarrer un nouveau timer pour rendre le texte invisible après 3 secondes
            refereeTimer = new DispatcherTimer();
            refereeTimer.Interval = TimeSpan.FromSeconds(3);
            refereeTimer.Tick += RefereeTimer_Tick;
            refereeTimer.Start();
        }

        private void Save()
        {
            ActionText = "Save !";
            ActionTextVisibility = Visibility.Visible;
            WhistleImageVisibility = Visibility.Visible;
            OnPropertyChanged(nameof(ActionTextVisibility));
            OnPropertyChanged(nameof(WhistleImageVisibility));

            // Arrêter le timer précédent s'il était en cours
            if (refereeTimer != null && refereeTimer.IsEnabled)
            {
                refereeTimer.Stop();
            }

            // Démarrer un nouveau timer pour rendre le texte invisible après 3 secondes
            refereeTimer = new DispatcherTimer();
            refereeTimer.Interval = TimeSpan.FromSeconds(3);
            refereeTimer.Tick += RefereeTimer_Tick;
            refereeTimer.Start();

            // Arrêter le timer du déplacement automatique du Goal
            goalTimer.Stop();
        }

        private void Result()
        {

        }
    }
}
