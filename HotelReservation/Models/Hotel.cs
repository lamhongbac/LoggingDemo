using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
  public  class Hotel
    {
        public Hotel(string _name,  ReservationBook _reservations)
        {
            Reservations = _reservations;
            Name = _name;
        }
        string name;//Hotel Name
        ReservationBook reservations; //so lưu trữ book phòng của hotel

        public ReservationBook Reservations { get => reservations; set => reservations = value; }
        public string Name { get => name; set => name = value; }
        //Get ReservationList
        public async Task <IEnumerable<Reservation>> GetUserReservation(string userName)
        {
            return await Reservations.GetReservationsForUser(userName);
        }
        //Create A reservation
        public async Task<IEnumerable<Reservation>> GetAllReservation()
        {
            return await Reservations.GetAllReservations(); ;
        }
        public async Task  CreateReservation(Reservation _reservation)
        {
           await Reservations.AddReservation(_reservation);
        }
    }
}
