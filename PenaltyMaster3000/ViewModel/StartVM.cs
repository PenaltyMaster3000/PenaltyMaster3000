using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PenaltyMaster3000.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PenaltyMaster3000.ViewModel
{
    public class StartVM : ObservableObject
    {

        private readonly INavigationService _navigationService;

        public ICommand StartCommand { get; set; }

        public StartVM(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            StartCommand = new RelayCommand(Start);
        }

        private void Start()
        {
            _navigationService.NavigateTo("MainView");
        }
    }
}
