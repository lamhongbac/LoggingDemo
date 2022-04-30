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
    public class NavigateCommand : CommandBase
    {
        NavigationService _navigationService;
        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object parameter)
        {

            _navigationService.Navigate();
        }
    }
}
