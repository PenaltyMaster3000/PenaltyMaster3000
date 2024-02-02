using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PenaltyMaster3000.ViewModel
{
    public class MainVM : ObservableObject
    {
        private DispatcherTimer timer = new DispatcherTimer();

        private bool image1Active = true;

        public BitmapImage CurrentImageSource
        {
            get
            {
                string imagePath = image1Active ? "/Images/goalkeeper_starting1.png" : "/Images/goalkeeper_starting2.png";
                return new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }
        }

        public MainVM() 
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            image1Active = !image1Active;
            OnPropertyChanged(nameof(CurrentImageSource));
        }
    }
}
