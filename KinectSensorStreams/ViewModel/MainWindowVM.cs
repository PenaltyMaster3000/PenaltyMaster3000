using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinectSensorStreams.ViewModel
{
    public class MainWindowVM
    {
        #region Properties

        public Color EllipseColor { get; set; }

        public ICommand StartCommand { get; set; }

        public ICommand StopCommand { get; set; }

        #endregion

        #region Constructor 

        public MainWindowVM() 
        {
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
        }

        #endregion

        #region Methods

        private void Start()
        {

            EllipseColor = Color.Green;
        }

        private void Stop()
        {

            EllipseColor = Color.Red;
        }

        #endregion
    }
}
