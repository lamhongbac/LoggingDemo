using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
  public  class Hotel
    {
        public Hotel(string _name)
        {
            Reservations = new ReservationBook();
            Name = _name;
        }
        string name;//Hotel Name
        ReservationBook reservations; //so lưu trữ book phòng của hotel

        public ReservationBook Reservations { get => reservations; set => reservations = value; }
        public string Name { get => name; set => name = value; }
        //Get ReservationList
        public IEnumerable<Reservation> GetUserReservation(string userName)
        {
            return Reservations.GetReservationsForUser(userName);
        }
        //Create A reservation

        public void CreateReservation(Reservation _reservation)
        {
            reservations.AddReservation(_reservation);
        }
    }
}
