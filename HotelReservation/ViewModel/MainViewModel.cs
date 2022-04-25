using HotelReservation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.ViewModel
{
   public class MainViewModel:ViewModelBase
    {
        NavigationStore navigationStore;
        public ViewModelBase CurrentViewModel { get=> navigationStore.CurrentViewModel; }
        public MainViewModel(NavigationStore _navigationStore)
        {
            navigationStore = _navigationStore;
              // CurrentViewModel = new ReservationListingViewModel();// MakeReservationViewModel();
        }
    }
}
