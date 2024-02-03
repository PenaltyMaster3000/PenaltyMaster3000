using PenaltyMaster3000.View;
using PenaltyMaster3000.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenaltyMaster3000.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly StartView _startView;

        public NavigationService(StartView startView)
        {
            _startView = startView;
        }

        public void NavigateTo(string pageKey)
        {
            switch(pageKey)
            {
                case "MainView":
                    MainView mainView = new MainView();
                    _startView.Content = mainView;
                    break;

                default:
                    throw new ArgumentException($"PageKey {pageKey} non prise en charge.");
            }
        }
    }
}
