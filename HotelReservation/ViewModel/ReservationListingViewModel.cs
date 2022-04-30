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
    public class ReservationListingViewModel : ViewModelBase
    {
        Hotel hotel;

        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public Task Initialization { get; private set; }
        public ICommand MakeReservationCommand { get; }
        public ICommand loadReservationCommand { get; }
        public ReservationListingViewModel(Hotel _hotel, NavigationService navigationService)
        {
            hotel = _hotel;
            //navigationStore = _navigationStore;

            MakeReservationCommand = new NavigateCommand(navigationService);
            _reservations = new ObservableCollection<ReservationViewModel>();
            loadReservationCommand = new LoadReservationCommand(this, hotel);

            //Initialization= UpdateReservation();
        }
        public static ReservationListingViewModel LoadViewModel(Hotel hotel,
            NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotel, makeReservationNavigationService);
            viewModel.loadReservationCommand.Execute(null);
            return viewModel;
        }
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
