using HotelReservation.Stores;
using HotelReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Services
{
    /// <summary>
    /// view config chi can viewmodel la biet chon lua view de ket hop hien thi
    /// Navigation Store giong nhu 1 hub duy nhat cung cap current view
    /// Navigation Service cung cap 1 nơi bao gồm hàm xác dinh view (Func{TView}), 
    /// sau do ket noi view do vao trong NAV store, 
    /// NAV se co event de thong bao cho MainViewModel, 
    /// de MainViewModel biet dc la co thay doi ViewModel
    /// </summary>
  public  class NavigationService<TViewModel> where TViewModel:ViewModelBase
    {
        NavigationStore navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        public NavigationService(NavigationStore _navigationStore, Func<TViewModel> createViewModel)
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
