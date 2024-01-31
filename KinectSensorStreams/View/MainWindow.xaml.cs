using KinectSensorStreams.ViewModel;
using System.Windows;

namespace KinectSensorStreams.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties

        public MainWindowVM MainWindowVM { get; set; }

        #endregion

        #region Constructor

        public MainWindow()
        {
            MainWindowVM = new MainWindowVM();
            InitializeComponent();
            DataContext = MainWindowVM;
        }

        #endregion
    }
}
