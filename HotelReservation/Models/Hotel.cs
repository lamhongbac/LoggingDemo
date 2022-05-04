using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    /// <summary>
    /// Hotel chua ben trong no 1 Reservation Book, giong nhu 1 so ghi chep
    /// Cac hoat dong cua hotel chinh la cac hoat dong cua Reservation book nay
    /// Co 2 hoat dong la , tra ve danh sach cac reservation va hd, dang ky phong (reservation)
    /// Reservation Book duoc dua vao doi tuong hotel thong qua constructor
    /// ROLES:
    /// Reservation Book la logic center cua ung dung, lo bao gom trong no cac reservation 
    /// Create reversation->Reservation book->hotel(room col)
    /// Con HotelStore la LocalData store cua ung dung, de giam thieu viec lien tuc doc ghi vao CSDL
    /// </summary>
  public  class Hotel
    {
        public Hotel(string _name,  ReservationBook _reservations)
        {
            HotelReservationBook = _reservations;
            Name = _name;
        }
        private string name;//Hotel Name
       private ReservationBook reservations; //so lưu trữ book phòng của hotel

        public ReservationBook HotelReservationBook { get => reservations; set => reservations = value; }
        public string Name { get => name; set => name = value; }
        //Get ReservationList
        public async Task <IEnumerable<Reservation>> GetUserReservation(string userName)
        {
            return await HotelReservationBook.GetReservationsForUser(userName);
        }
       
        public async Task<IEnumerable<Reservation>> GetAllReservation()
        {
            return await HotelReservationBook.GetAllReservations(); ;
        }
        public async Task  CreateReservation(Reservation _reservation)
        {
           await HotelReservationBook.AddReservation(_reservation);
        }
    }
}
