using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Stores;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Command
{
    /// <summary>
    /// thuc hien vai tro set lai current viewmodel 
    /// </summary>
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel:ViewModelBase

    {
        NavigationService<TViewModel> _navigationService;
        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object parameter)
        {

            _navigationService.Navigate();
        }
    }
}
