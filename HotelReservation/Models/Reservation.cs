using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    /// <summary>
    /// Information of 
    /// a Reservation to a hotel
    /// </summary>
    public class Reservation
    {
        public Reservation(RoomID _room,DateTime _startTime, DateTime _endTime, string _userName)
        {
            room = _room;
            StartTime = _startTime;
            EndTime = _endTime;
            UserName = _userName;
            
        }
        //kg can chi dinh Hotel vi reservation nam ben trong ReservationBook ma RB nay inside 1 hotel
        RoomID room; //book phong nao : tham chieu den bang danh muc phong
        //string custName; //Ai: khach hang ten gi - tham chieu bang danh muc kh
        DateTime startTime; //bat dau luc nao
        DateTime endTime; //ket thuc luc nao
        string userName;//who make a reservation
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        //public string CustName { get => custName; set => custName = value; }
        public RoomID Room { get => room; set => room = value; }
        public string UserName { get => userName; set => userName = value; }
        public TimeSpan Length => EndTime.Subtract(StartTime);

        public bool IsConflicted(Reservation newReservation)
        {
            if (newReservation != null)
            {
                if (this.Room.Equals(newReservation.Room) && (newReservation.StartTime < EndTime || newReservation.EndTime > StartTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
