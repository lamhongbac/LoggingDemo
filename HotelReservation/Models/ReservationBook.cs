using HotelReservation.DbContexts;
using HotelReservation.Exceptions;
using HotelReservation.Services.ReservationProvider;
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
        IReservationCreator reservationCreator;
        IReservationProvider reservationProvider;
        IReservationConflictValidator reservationConflict;
       public ReservationBook(IReservationCreator _reservationCreator, 
           IReservationProvider _reservationProvider, 
           IReservationConflictValidator _reservationConflict)
        {
            //ReserRoomDbContextFactory dbContextFactory=new  ReserRoomDbContextFactory(connectionString)
            reservationCreator = _reservationCreator;
            reservationProvider = _reservationProvider;
            reservationConflict = _reservationConflict;
        }
       //List<Reservation> _reservations;
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await reservationProvider.GetAllReservations();
        }
        public async Task<IEnumerable<Reservation>> GetReservationsForUser (string userName)
        {
            IEnumerable<Reservation> reservations = await reservationProvider.GetAllReservations();

            return reservations.Where(x=>x.UserName==userName);
        }
        //public List<Reservation> Reservations { get => _reservations; set => _reservations = value; }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictReservation = await reservationConflict.GetConflictReservation(reservation);
            //kiem tra xem book co bi conflict khong
            if (conflictReservation!=null)
            {
                throw new ReservationConflictException(reservation, conflictReservation);
            }
           
                await reservationCreator.CreateReservation(reservation);
            
            //foreach (var existReservation in Reservations)
            //{
            //    if (existReservation.IsConflicted(reservation))
            //    {
            //        throw new ReservationConflictException(existReservation, reservation, "booking room is conflicted with the exist one");
            //    }

            //}

           




        }
    }
}
