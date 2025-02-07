﻿using PenaltyMaster3000.ViewModel;
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

namespace PenaltyMaster3000.View
{
    /// <summary>
    /// Logique d'interaction pour MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainVM MainVM { get; set; }

        public MainView()
        {
            MainVM = new MainVM();
            InitializeComponent();
            DataContext = MainVM;
        }
    }
}
