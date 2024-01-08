using KinectSensorStreams.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public override void BeginInit()
        {
            base.BeginInit();
            MainWindowVM.StartCommand.Execute(null);
        }

        #endregion
    }
}
