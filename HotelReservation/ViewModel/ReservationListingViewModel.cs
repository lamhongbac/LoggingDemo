using HotelReservation.Command;
using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.ViewModel
{
  public  class ReservationListingViewModel:ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand MakeReservationCommand { get; }
        public ReservationListingViewModel()
        {

            MakeReservationCommand = new NavigationCommand();
               _reservations = new ObservableCollection<ReservationViewModel>();
            Reservation reservation1 = new Reservation(new RoomID(1, 2), new DateTime(2022, 04, 24), new DateTime(2022, 02, 25), "Lam Hong Bac", "Cust1");
            Reservation reservation2 = new Reservation(new RoomID(2, 2), new DateTime(2022, 04, 25), new DateTime(2022, 02, 26), "Hoang Dat", "Cust2");
            Reservation reservation3 = new Reservation(new RoomID(1, 1), new DateTime(2022, 04, 25), new DateTime(2022, 02, 26), "Nghinh Thu", "Cust3");
            Reservation reservation4 = new Reservation(new RoomID(3, 1), new DateTime(2022, 04, 26), new DateTime(2022, 02, 27), "Bao Trang", "Cust4");

            _reservations.Add(new ReservationViewModel(reservation1));
            _reservations.Add(new ReservationViewModel(reservation2));
            _reservations.Add(new ReservationViewModel(reservation3));
            _reservations.Add(new ReservationViewModel(reservation4));

        }

    }
}
