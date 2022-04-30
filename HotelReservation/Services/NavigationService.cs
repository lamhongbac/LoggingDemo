using HotelReservation.Stores;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services
{
  public  class NavigationService
    {
        NavigationStore navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;
        public NavigationService(NavigationStore _navigationStore, Func<ViewModelBase> createViewModel)
        {
            navigationStore = _navigationStore;
            _createViewModel = createViewModel;
        }
        public  void Navigate()
        {

            navigationStore.CurrentViewModel = _createViewModel();
        }
       
    }
}
