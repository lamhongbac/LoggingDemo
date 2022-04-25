using HotelReservation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Models
{
    /// <summary>
    /// danh sach / quyen so chứa các reservations 
    /// </summary>
   public class ReservationBook
    {
       public ReservationBook()
        {
            Reservations = new List<Reservation>();
        }
       List<Reservation> _reservations;

        public IEnumerable<Reservation> GetReservationsForUser (string userName)
        {
            return Reservations.Where(x=>x.UserName==userName);
        }
        public List<Reservation> Reservations { get => _reservations; set => _reservations = value; }

        internal void AddReservation(Reservation reservation)
        {
            try
            {
                //kiem tra xem book co bi conflict khong

                foreach (var existReservation in Reservations)
                {
                    if (existReservation.IsConflicted(reservation))
                    {
                        throw new ReservationConflictException(existReservation, reservation,"booking room is conflicted with the exist one");
                    }
                }
            }
            catch
            {
                Reservations.Add(reservation);
            }
            
            
        }
    }
}
