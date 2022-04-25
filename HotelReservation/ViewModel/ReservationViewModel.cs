using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservation.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
       private readonly Reservation reservation;
        public string StartTime => reservation.StartTime.ToString("d");
        public string EndTime => reservation.EndTime.ToString("d");
        public string RoomID => reservation.Room?.ToString();
        public string UserName => reservation.UserName;
              
        public ReservationViewModel(Reservation _reservation)
        {
            reservation = _reservation;
        }
    }
}
