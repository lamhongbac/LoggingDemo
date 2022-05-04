using HotelReservation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.ViewModel
{
    /// <summary>
    /// cung cap data cho mainview bao gom view model cho cac user control ben trong main view
    /// mainView(UserControl)=> mainViewModel(ViewModel)
    /// cung cap current viewmodel thong navigationstore
    /// 
    /// </summary>
   public class MainViewModel:ViewModelBase
    {
        NavigationStore navigationStore;
        public ViewModelBase CurrentViewModel { get=> navigationStore.CurrentViewModel; }
        public MainViewModel(NavigationStore _navigationStore)
        {
            navigationStore = _navigationStore;
            navigationStore.CurrentViewModelChanged += NavigationStore_CurrentViewModelChanged;
              // CurrentViewModel = new ReservationListingViewModel();// MakeReservationViewModel();
        }
        /// <summary>
        /// trigger viec goi event property CurrentViewModel change/ 
        /// theo thong le event nay dc goi boi method set nhu sau set{ CurrentViewModel=value;PropertyChanged();}
        /// o day chi co thuoc tinh Get , va no chi la cai hub con thuc su CurrentViewModel chi la o 
        /// navigation.CurrentViewModel
        /// </summary>
        private void NavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
