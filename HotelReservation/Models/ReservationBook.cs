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
    /// danh sach / quyen so chứa các reservations hay chua cac booking 
    /// 1 cong ty - 1 reservation book
    /// implemented DataHandler dc khoi tao va thiet lap ben ngoai Book va dua vao thong qua constructor 
    /// or Set method, data type ma DataHandler tra ve luon la logic object
    /// Co 2 hoat dong la , tra ve danh sach cac reservation (GetReservation) 
    /// va hd, dang ky phong (MakeReservation)
    /// </summary>
    public class ReservationBook
    {
        #region DataHandler
        IReservationCreator reservationCreator;
        IReservationProvider reservationProvider;
        IReservationConflictValidator reservationConflict; 
        #endregion
        public ReservationBook(IReservationCreator _reservationCreator,
            IReservationProvider _reservationProvider,
            IReservationConflictValidator _reservationConflict)
        {
            //ReserRoomDbContextFactory dbContextFactory=new  ReserRoomDbContextFactory(connectionString)
            reservationCreator = _reservationCreator;
            reservationProvider = _reservationProvider;
            reservationConflict = _reservationConflict;
        }
        
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await reservationProvider.GetAllReservations();
        }
        public async Task<IEnumerable<Reservation>> GetReservationsForUser(string userName)
        {
            IEnumerable<Reservation> reservations = await reservationProvider.GetAllReservations();
            return reservations.Where(x => x.UserName == userName);
        }
        //public List<Reservation> Reservations { get => _reservations; set => _reservations = value; }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictReservation = await reservationConflict.GetConflictReservation(reservation);
            //kiem tra xem book co bi conflict khong
            if (conflictReservation != null)
            {
                throw new ReservationConflictException(reservation, conflictReservation);
            }
            await reservationCreator.CreateReservation(reservation);
        }
    }
}
