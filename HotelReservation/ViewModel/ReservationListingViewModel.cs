using HotelReservation.Command;
using HotelReservation.Models;
using HotelReservation.Services;
using HotelReservation.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.ViewModel
{
    /// <summary>
    /// View Model cho view List Reservation View
    /// list data trong View model la nguyen lieu de hien thi trong viev
    /// list data trong View model can dam bao dong nhat (syncronize) voi list data cua Local Store
    /// 
    /// </summary>
    public class ReservationListingViewModel : ViewModelBase
    {
        //cung cap data cho this object
        HotelStore hotelStore; 

        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        
        bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set 
            { 
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand MakeReservationCommand { get; }
        //
        //tach viec load data tu DB len doi tuong ra khoi constructor
        //Usage: sau khi khoi tao xong viewModel thuc hien load data nhu sau
        // viewModel.LoadReservationCommand(null);
        //
        public ICommand LoadReservationCommand { get; }
        /// <summary>
        /// trong TH make reserver view co mat cung voi reser listview
        /// </summary>
        public MakeReservationViewModel MakeReservationViewModel { get; }

        public ReservationListingViewModel(HotelStore _hotelStore,
            MakeReservationViewModel makeReservationViewModel,
            NavigationService<ReservationListingViewModel> navigationService)
        {
            hotelStore = _hotelStore;
            //navigationStore = _navigationStore;
            MakeReservationViewModel = makeReservationViewModel;
            MakeReservationCommand = new NavigateCommand<ReservationListingViewModel>(navigationService);
            _reservations = new ObservableCollection<ReservationViewModel>();
            LoadReservationCommand = new LoadReservationCommand(this, hotelStore);
            hotelStore.ReservationMade += HotelStore_ReservationMade;
           
        }
        public override void Dispose()
        {
            hotelStore.ReservationMade -= HotelStore_ReservationMade;
            base.Dispose();
        }


        //~ReservationListingViewModel()
        //{

        //}


        /// <summary>
        /// Hàm lắng nghe hành động Add reservation vào local store/ HotelStore
        /// neu co add thi add vao trong lưới thông qua:_reservations viewmodel
        /// new ReservationViewModel(Reservation obj)
        /// </summary>
        /// <param name="obj"></param>
        private void HotelStore_ReservationMade(Reservation obj)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(obj);
            _reservations.Add(reservationViewModel);
        }
        /// <summary>
        /// hàm tĩnh:dùng để tạo ra ReservationListingViewModel + lấy data (ds ReservationViewModel) tu CSDL 
        /// day la 1 ky thuật tránh việc thuc hien lay data ben trong constructor cua ReservationListingViewModel 
        /// Usage: ReservationListingViewModel viewModel=ReservationListingViewModel.LoadViewModel();
        /// </summary>
        /// <param name="hotelStore"></param>
        /// <param name="makeReservationViewModel"></param>
        /// <param name="makeReservationNavigationService"></param>
        /// <returns></returns>
        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, 
            MakeReservationViewModel makeReservationViewModel,
            NavigationService<ReservationListingViewModel> makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationViewModel, makeReservationNavigationService);
            viewModel.LoadReservationCommand.Execute(null);
            return viewModel;
        }
        /// <summary>
        /// Ham dung de update ds reservation logic object 
        /// tu HotelStore vao ds reservation viewmodel
        /// Usage:reservationListingViewModel.Update(HotelStore.reservation)
        /// Uc: LoadDataCommand
        /// 
        /// </summary>
        /// <param name="Reservations"></param>
        public void UpdateReservation(IEnumerable<Reservation> Reservations)
        {
            _reservations.Clear();
            //IEnumerable<Reservation> Reservations =await hotel.Reservations.GetAllReservations();
            if (Reservations==null||Reservations.Count()==0)
            {
                return;
            }
            foreach (var item in Reservations)
            {
                _reservations.Add(new ReservationViewModel(item));
            }

        }
    }
}
